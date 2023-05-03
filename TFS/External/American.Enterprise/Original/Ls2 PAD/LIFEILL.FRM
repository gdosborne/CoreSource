VERSION 2.00
Begin Form frmLIFEILL 
   Caption         =   "ILLUSTRATION PREVIEW"
   ClientHeight    =   7740
   ClientLeft      =   60
   ClientTop       =   705
   ClientWidth     =   11880
   FontBold        =   0   'False
   FontItalic      =   0   'False
   FontName        =   "MS Sans Serif"
   FontSize        =   9.75
   FontStrikethru  =   0   'False
   FontUnderline   =   0   'False
   Height          =   8430
   Left            =   0
   LinkTopic       =   "Form2"
   ScaleHeight     =   12.985
   ScaleMode       =   0  'User
   ScaleWidth      =   20.189
   Top             =   75
   Width           =   12000
   Begin TextBox txtPrinting 
      BorderStyle     =   0  'None
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   255
      Left            =   0
      TabIndex        =   1
      TabStop         =   0   'False
      Top             =   480
      Visible         =   0   'False
      Width           =   9255
   End
   Begin TextBox txtIllusPage 
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   7695
      Left            =   0
      MultiLine       =   -1  'True
      ScrollBars      =   3  'Both
      TabIndex        =   0
      Top             =   0
      Width           =   11895
   End
   Begin CommonDialog CMDialog1 
      Left            =   480
      Top             =   120
   End
   Begin Menu mnuPrint 
      Caption         =   "&Print"
      Index           =   0
      Begin Menu mnuPrintIllus 
         Caption         =   "Print "
      End
      Begin Menu mnuPrintSetup 
         Caption         =   "Printer Setup"
      End
      Begin Menu mnuPrintFonts 
         Caption         =   "Printer Fonts"
      End
   End
   Begin Menu mnuExit 
      Caption         =   "E&xit"
      Index           =   1
   End
   Begin Menu mnuHelp 
      Caption         =   "&Help"
      Index           =   2
   End
End

Sub Form_Load ()

    msgfile$ = "\illprhlp.msg"
    NL$ = Chr$(13) & Chr$(10)
    PrintIllusPage(0) = "P1"
    PrintIllusPage(1) = "P2"
    PrintIllusPage(2) = "P3"
    PrintIllusPage(3) = "P4"
    PrintIllusPage(4) = "P5"
    PrintIllusPage(5) = "P6"
    txtIllusPage.Text = ""
    If defPrintFont <> "" Then
       txtIllusPage.FontName = defPrintFont
       txtIllusPage.FontSize = 10    'always
       txtIllusPage.FontBold = defFontBold
       txtIllusPage.FontItalic = defFontItal
    Else
       txtIllusPage.FontName = "Courier"
       txtIllusPage.FontSize = 10
       txtIllusPage.FontBold = False
       txtIllusPage.FontItalic = False
    End If
    
    pagetext$ = ""
    PrintIllusPage(0) = ""
    LIFEPAGE = 1
    Call LifeSave_Illus_Head(pagetext$, True)
    'PrintIllusPage(0) = PrintIllusPage(0) & pagetext$
    txtIllusPage.Text = txtIllusPage.Text & pagetext$
    pagetext$ = ""
    Call LifeSave_Ledger(pagetext$)
    'PrintIllusPage(LIFEPAGE - 1) = PrintIllusPage(LIFEPAGE - 1) & pagetext$
    txtIllusPage.Text = txtIllusPage.Text & pagetext$
    pagetext$ = ""
    Call LifeSave_Close_Text(pagetext$, 1, LSClient(SpouseClient).CurrRate)
    'PrintIllusPage(LIFEPAGE - 1) = PrintIllusPage(LIFEPAGE - 1) & pagetext$
    txtIllusPage.Text = txtIllusPage.Text & pagetext$

End Sub

Sub Form_Unload (Cancel As Integer)

    Set frmLIFEILL = Nothing

End Sub

Sub mnuExit_Click (Index As Integer)

    Unload frmLIFEILL
    msgfile$ = SaveMsgFile

End Sub

Sub mnuHelp_Click (Index As Integer)

    'filemsg msgfile$, 1

End Sub

Sub mnuPrintFonts_Click ()

    On Error GoTo ErrFontHandler
    CMDialog1.CancelError = True

    CMDialog1.FontName = defPrintFont
    CMDialog1.FontSize = defFontSiz
    CMDialog1.FontBold = defFontBold
    CMDialog1.FontItalic = defFontItal
    
    CMDialog1.Min = 10
    CMDialog1.Max = 12
    CMDialog1.Flags = CF_PRINTERFONTS Or CF_APPLY Or CF_LIMITSIZE Or CF_FIXEDPITCHONLY
    'Added to limit FontSize.
    CMDialog1.Action = 4
    defPrintFont = CMDialog1.FontName
    defFontSiz = CMDialog1.FontSize
    defFontBold = CMDialog1.FontBold
    defFontItal = CMDialog1.FontItalic
    txtIllusPage.FontName = CMDialog1.FontName
    txtIllusPage.FontSize = 10    'always
    txtIllusPage.FontBold = CMDialog1.FontBold
    txtIllusPage.FontItalic = CMDialog1.FontItalic
    'Added to Write FontName and size to INI file.
    RetStr$ = CMDialog1.FontName
    i% = WriteAnyIniStr("Americare", "LFont", RetStr$, "AMRICARE.INI")
    RetStr$ = Trim$(Str$(CMDialog1.FontSize))
    i% = WriteAnyIniStr("Americare", "LFontSiz", RetStr$, "AMRICARE.INI")
    
ErrFontHandler:
    txtIllusPage.SetFocus
    Exit Sub
End Sub

Sub mnuPrintIllus_Click ()

    On Local Error GoTo ErrPrinting

    frmLIFEILL.MousePointer = 11
    If CMDialog1.FontName = "" Then
       Printer.FontName = "Courier"
       Printer.FontSize = 12
       Printer.FontBold = True
       Printer.FontItalic = False
    Else
       Printer.FontName = CMDialog1.FontName
       Printer.FontSize = CMDialog1.FontSize
       Printer.FontBold = CMDialog1.FontBold
       Printer.FontItalic = CMDialog1.FontItalic
    End If
    txtIllusPage.Visible = 0
    txtPrinting.Visible = 1
    StartTime = Timer
    While (StartTime + 1.5 > Timer)
    Wend
    txtPrinting.Visible = 0
    txtIllusPage.Visible = 1

    For i% = 0 To 5
      If Left$(PrintIllusPage(i%), 1) <> "P" Then
         Printer.CurrentX = 0
         Printer.CurrentY = 0
         PageLength% = Len(PrintIllusPage(i%))
         j% = 1
         While j% < PageLength%
            RetCd% = PrintALine(j%, PrintIllusPage(i%), oneline$)
            If RetCd% = True Then
               Printer.Print oneline$;
               j% = j% + Len(oneline$) + 2
            End If
            Printer.CurrentX = 0
            If Printer.FontSize < 11 Then  '8 lines per inch
               Printer.CurrentY = Printer.CurrentY + 180
            Else
               If TTInfo.IllusType = 2 Then
                  Printer.CurrentY = Printer.CurrentY + 240  '6 lpi
               Else
                  Printer.CurrentY = Printer.CurrentY + 225  '6.4 lpi
               End If
            End If
         Wend
         Printer.NewPage
      End If
    Next i%
    Printer.EndDoc
    frmLIFEILL.MousePointer = 0
    txtIllusPage.SetFocus
    Exit Sub

ErrPrinting:

    Select Case Err
       Case 380
          msgline$ = "The font selected for printing is not on your system.  Please select another valid print font."
          MsgBox msgline$
          Call mnuPrintFonts_Click
          Resume Next
       Case Else
          msgline$ = "Error '" & Error$(Err) & "' occurred; check printer and font setup."
          MsgBox msgline$
          txtIllusPage.SetFocus
          Exit Sub
    End Select
    
End Sub

Sub mnuPrintSetup_Click ()

    CMDialog1.CancelError = True
    On Error GoTo ErrHandler

    CMDialog1.Flags = &H40&
    CMDialog1.Action = 5

ErrHandler:
    txtIllusPage.SetFocus
    Exit Sub

End Sub

Sub txtIllusPage_KeyDown (KeyCode As Integer, Shift As Integer)

    'If KeyCode = KEY_F1 Then filemsg msgfile$, 1

End Sub

Sub txtIllusPage_KeyPress (KeyAscii As Integer)

    KeyAscii = 0

End Sub

