VERSION 2.00
Begin Form frmVSPrint 
   BackColor       =   &H00C0C0C0&
   BorderStyle     =   1  'Fixed Single
   Caption         =   "LifeSavings Illustration Preview"
   ClientHeight    =   6915
   ClientLeft      =   60
   ClientTop       =   420
   ClientWidth     =   9585
   Height          =   7320
   Left            =   0
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   6915
   ScaleWidth      =   9585
   Top             =   75
   Width           =   9705
   Begin SSCommand cmdFonts 
      Caption         =   "Fon&ts"
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   8.25
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   495
      Left            =   1920
      TabIndex        =   19
      Top             =   120
      Width           =   620
   End
   Begin CommonDialog CMDialog1 
      Left            =   2760
      Top             =   240
   End
   Begin SSCheck chkMono 
      Alignment       =   1  'Right Justify
      Caption         =   "Force Preview to Monochrome"
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "Small Fonts"
      FontSize        =   6.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   195
      Left            =   0
      TabIndex        =   4
      Top             =   1800
      Value           =   -1  'True
      Width           =   2535
   End
   Begin SSCommand cmdPreview 
      Caption         =   "Previe&w Certif"
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   8.25
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   495
      Index           =   1
      Left            =   1320
      TabIndex        =   1
      Top             =   600
      Width           =   1215
   End
   Begin SSCommand cmdPrint 
      Caption         =   "Print &Certification Only "
      Enabled         =   0   'False
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "Small Fonts"
      FontSize        =   6
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   195
      Index           =   1
      Left            =   0
      TabIndex        =   3
      Top             =   1560
      Visible         =   0   'False
      Width           =   2535
   End
   Begin SSCommand cmdPrintSetup 
      Caption         =   "Printer &Setup"
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   8.25
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   495
      Left            =   720
      TabIndex        =   10
      Top             =   120
      Width           =   1200
   End
   Begin vsPrinter vsPrint 
      ConvInfo        =   VSPRINT.FRX:0000
      FontBold        =   -1  'True
      FontName        =   "Small Fonts"
      FontSize        =   7
      HdrFontName     =   "Courier New"
      HdrFontSize     =   14
      Height          =   6750
      Left            =   2640
      PreviewMode     =   2  'Force Monochrome
      Top             =   120
      Width           =   6855
   End
   Begin Frame fraHelp 
      BackColor       =   &H00C0C0C0&
      Height          =   4815
      Left            =   0
      TabIndex        =   13
      Top             =   2040
      Width           =   2535
      Begin CommandButton cmdLastPage 
         Caption         =   ">>"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   495
         Left            =   2070
         TabIndex        =   8
         Top             =   4200
         Width           =   400
      End
      Begin CommandButton cmdFirstPage 
         Caption         =   "<<"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   495
         Left            =   40
         TabIndex        =   5
         Top             =   4200
         Width           =   400
      End
      Begin CommandButton cmdPageForward 
         Caption         =   " &Forward >"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   495
         Left            =   1190
         TabIndex        =   7
         Top             =   4200
         Width           =   880
      End
      Begin CommandButton cmdPageBack 
         Caption         =   "< &Back"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   495
         Left            =   440
         TabIndex        =   6
         Top             =   4200
         Width           =   750
      End
      Begin HScrollBar hscZoom 
         Height          =   255
         LargeChange     =   25
         Left            =   480
         Max             =   175
         Min             =   25
         SmallChange     =   5
         TabIndex        =   9
         Top             =   2760
         Value           =   25
         Width           =   1455
      End
      Begin Label Label1 
         BackColor       =   &H00C0C0C0&
         Caption         =   "OUT"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   3
         Left            =   2040
         TabIndex        =   12
         Top             =   2760
         Width           =   375
      End
      Begin Label Label1 
         BackColor       =   &H00C0C0C0&
         Caption         =   "IN"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   0
         Left            =   120
         TabIndex        =   18
         Top             =   2760
         Width           =   375
      End
      Begin Label lblPreview 
         BackColor       =   &H00C0C0C0&
         Caption         =   "To move from page to page, press the SHIFT key while double-clicking the left or right mouse or use the buttons below. (Also Alt-B or Alt-F on keyboard)"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   975
         Index           =   3
         Left            =   120
         TabIndex        =   17
         Top             =   3120
         Width           =   2295
      End
      Begin Label lblPreview 
         BackColor       =   &H00C0C0C0&
         Caption         =   "To zoom in or out on the current page, double-click the left or right mouse buttons or use scroll below."
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   855
         Index           =   2
         Left            =   120
         TabIndex        =   16
         Top             =   1920
         Width           =   2295
      End
      Begin Label lblPreview 
         BackColor       =   &H00C0C0C0&
         Caption         =   "To move around the current page, click and drag the mouse or use the scroll bars to the right and bottom of the page."
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   975
         Index           =   1
         Left            =   120
         TabIndex        =   15
         Top             =   960
         Width           =   2295
      End
      Begin Label lblPreview 
         BackColor       =   &H00C0C0C0&
         Caption         =   "Press ENTER or ""Preview Illus"" to display the Life Savings Illustration at right ---> "
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   615
         Index           =   0
         Left            =   120
         TabIndex        =   14
         Top             =   240
         Width           =   2295
      End
   End
   Begin SSCommand cmdPreview 
      Caption         =   "Pre&view Illus"
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   8.25
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   495
      Index           =   0
      Left            =   0
      TabIndex        =   0
      Top             =   600
      Width           =   1335
   End
   Begin SSCommand cmdReturn 
      Caption         =   "&Return"
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   8.25
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   495
      Left            =   0
      TabIndex        =   11
      Top             =   120
      Width           =   735
   End
   Begin SSCommand cmdPrint 
      Caption         =   "&Print Illustration"
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   8.25
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   495
      Index           =   0
      Left            =   0
      TabIndex        =   2
      Top             =   1080
      Width           =   2535
   End
End

Option Explicit

    Dim CurrentPreview As Integer  '0=ill,1=certif

Sub chkMono_Click (Value As Integer)

    If Value = False Then
       If defShowMono = 0 Then
          MsgText = "Switching Preview Mode from 'Monochrome' to 'Screen Compatible'.  This will cause your previews to use up to 8 times more memory."
          MsgBox MsgText, , "LifeSavings"
       End If
       defShowMono = 1
       vsPrint.PreviewMode = 0
    Else
       If defShowMono <> 0 Then
          MsgText = "Switching Preview Mode from 'Screen Compatible' to 'Monochrome'.  Your previews will use less memory, but your preview background color should be set to 'white' or it may not be readable."
          MsgBox MsgText, , "LifeSavings"
       End If
       defShowMono = 0
       vsPrint.PreviewMode = 2
    End If

End Sub

Sub cmdFirstPage_Click ()
    
    frmVSPrint.MousePointer = 11
    vsPrint.PreviewPage = 1
    frmVSPrint.MousePointer = 0

End Sub

Sub cmdFonts_Click ()

    CMDialog1.Flags = CF_PRINTERFONTS
    CMDialog1.Action = 4
    If LS3FontDefault <> CMDialog1.FontName Then
       LS3FontDefault = CMDialog1.FontName
       RetCode = WriteAnyIniStr("Americare", "LS3Font", LS3FontDefault, "AMRICARE.INI")
    End If

End Sub

Sub cmdLastPage_Click ()

    frmVSPrint.MousePointer = 11
    vsPrint.PreviewPage = vsPrint.CurrentPage - 1  'Last page of illustration
    frmVSPrint.MousePointer = 0

End Sub

Sub cmdPageBack_Click ()
    
    If vsPrint.PreviewPage > 1 Then
       frmVSPrint.MousePointer = 11
       vsPrint.PreviewPage = vsPrint.PreviewPage - 1
       frmVSPrint.MousePointer = 0
    End If

End Sub

Sub cmdPageForward_Click ()

    If vsPrint.CurrentPage > vsPrint.PreviewPage Then
       frmVSPrint.MousePointer = 11
       vsPrint.PreviewPage = vsPrint.PreviewPage + 1
       frmVSPrint.MousePointer = 0
    End If
    
End Sub

Sub cmdPreview_Click (Index As Integer)

    vsPrint.Preview = True
    frmVSPrint.MousePointer = 11
    'VSPrint.StartDoc
    vsPrint.Action = 3
    If Index = 0 Then
       Call Print_Illustration
       Call Print_Certification(False)
       CurrentPreview = 0
    Else
       'Call Print_Certification(True)  /* removed "true" 06-97
       Call Print_Certification(False)
       CurrentPreview = 1
    End If
    'vsPrint.EndDoc
    vsPrint.Action = 6
    frmVSPrint.MousePointer = 0

End Sub

Sub cmdPrint_Click (Index As Integer)

    If defPrintCertif = 0 And Index = 1 Then
       MsgText = "Sorry, you are not authorized to print standalone certifications. Please print the entire illustration."
       MsgBox MsgText, , "LifeSavings"
       Exit Sub
    End If

    If vsPrint.Preview = True Then
       If Index = CurrentPreview Then
          vsPrint.Action = 11
          'vsPrint.PrintDoc
       Else
          vsPrint.Action = 3
          'vsPrint.StartDoc
          If Index = 0 Then
             Call Print_Illustration
             Call Print_Certification(False)
             CurrentPreview = 0
          Else
             'Call Print_Certification(True)  /* Removed "true" 06-97 */
             Call Print_Certification(False)
             CurrentPreview = 1
          End If
          vsPrint.Action = 6
          'vsPrint.EndDoc
          vsPrint.Action = 11
          'vsPrint.PrintDoc
       End If
    Else
       vsPrint.Action = 3
       'vsPrint.StartDoc
       If Index = 0 Then
          Call Print_Illustration
          Call Print_Certification(False)
       Else
          'Call Print_Certification(True)  /* removed "true" 06-97 */
          Call Print_Certification(False)
       End If
       vsPrint.Action = 6
       'vsPrint.EndDoc
       vsPrint.Action = 11
       'vsPrint.PrintDoc
    End If

End Sub

Sub cmdPrintSetup_Click ()

    If vsPrint.Preview = True Then
       If defPrintCertif = 0 And CurrentPreview = 1 Then
          MsgText = "You must preview the entire illustration before entering Printer Setup.  You are not authorized to print standalone certifications. Please print the entire illustration."
          MsgBox MsgText, , "LifeSavings"
          Exit Sub
       End If
       frmVSPrint.MousePointer = 11
       vsPrint.Action = 12
       'vsPrint.PrintDoc
       frmVSPrint.MousePointer = 0
    Else
       frmVSPrint.MousePointer = 11
       vsPrint.Preview = True
       vsPrint.Action = 3
       'vsPrint.StartDoc
       Call Print_Illustration
       Call Print_Certification(False)
       CurrentPreview = 0
       vsPrint.Action = 6
       'vsPrint.EndDoc
       vsPrint.Action = 12
       'vsPrint.PrintDoc
       frmVSPrint.MousePointer = 0
    End If

End Sub

Sub cmdReturn_Click ()

   Unload frmVSPrint
   
End Sub

Sub Form_Load ()

    Dim RetStr As String

    RetStr = GetAnyIniStr("Americare", "LS3Font", "AMRICARE.INI")
    If Trim$(RetStr) <> "" Then
       LS3FontDefault = RetStr
    Else
       LS3FontDefault = "Times New Roman"
    End If

    vsPrint.Preview = False
    CurrentPreview = 0
    vsPrint.Zoom = defZoomSize
    hscZoom.Value = defZoomSize
    If chkMono.Value = True Then
       If defShowMono <> 0 Then chkMono.Value = False
    Else
       If defShowMono = 0 Then chkMono.Value = True
    End If
    If defPrintCertif <> 0 Then
       cmdPrint(1).Enabled = True
       cmdPrint(1).Visible = True
    End If
    vsPrint.MouseScroll = True
    vsPrint.MouseZoom = True
    vsPrint.MousePage = True
  
End Sub

Sub Form_Unload (Cancel As Integer)

    Dim tmpINIValue As String
    
    If defZoomSize <> vsPrint.Zoom Then
       defZoomSize = vsPrint.Zoom
       tmpINIValue = Trim$(Str$(defZoomSize))
       RetCode = WriteAnyIniStr("Americare", "LS3Zoom", tmpINIValue, "AMRICARE.INI")
    End If
    If chkMono.Value = True Then
       defShowMono = 0
       tmpINIValue = Trim$(Str$(defShowMono))
       RetCode = WriteAnyIniStr("Americare", "ShowMono", tmpINIValue, "AMRICARE.INI")
    Else
       defShowMono = 1
       tmpINIValue = Trim$(Str$(defShowMono))
       RetCode = WriteAnyIniStr("Americare", "ShowMono", tmpINIValue, "AMRICARE.INI")
    End If
    Set frmVSPrint = Nothing
    
End Sub

Sub hscZoom_Change ()

    vsPrint.Zoom = hscZoom.Value
    
End Sub

Sub VSPrint_NewTableCell (Row As Integer, Column As Integer, Cell As String)

    If CertifTable <> 1 Then Exit Sub

    If Column = 3 Then
       frmVSPrint.vsPrint.FontBold = True
    Else
       frmVSPrint.vsPrint.FontBold = False
    End If

End Sub

