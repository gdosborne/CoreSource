VERSION 2.00
Begin Form frmLSILLTYP 
   Caption         =   "Type of Life Savings II Illustration"
   ClientHeight    =   4710
   ClientLeft      =   1530
   ClientTop       =   750
   ClientWidth     =   9120
   ControlBox      =   0   'False
   Height          =   5190
   Left            =   1470
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   4710
   ScaleWidth      =   9120
   Top             =   330
   Width           =   9240
   Begin Frame fraIllusType 
      Height          =   3615
      Left            =   120
      TabIndex        =   11
      Top             =   120
      Width           =   8895
      Begin TextBox txtIllusHighAge 
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   360
         Index           =   4
         Left            =   6840
         MaxLength       =   2
         TabIndex        =   6
         Top             =   1560
         Visible         =   0   'False
         Width           =   495
      End
      Begin TextBox txtIllusHighAge 
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   360
         Index           =   3
         Left            =   6120
         MaxLength       =   2
         TabIndex        =   5
         Top             =   1560
         Visible         =   0   'False
         Width           =   495
      End
      Begin TextBox txtIllusHighAge 
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   360
         Index           =   2
         Left            =   5400
         MaxLength       =   2
         TabIndex        =   4
         Top             =   1560
         Visible         =   0   'False
         Width           =   495
      End
      Begin TextBox txtIllusHighAge 
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   360
         Index           =   1
         Left            =   4680
         MaxLength       =   2
         TabIndex        =   3
         Top             =   1560
         Visible         =   0   'False
         Width           =   495
      End
      Begin TextBox txtIllusHighAge 
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   360
         Index           =   0
         Left            =   3960
         MaxLength       =   2
         TabIndex        =   2
         Top             =   1560
         Visible         =   0   'False
         Width           =   495
      End
      Begin OptionButton optIllusType 
         Caption         =   "Show first 10 years, then every 5 years until:"
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   12
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Index           =   2
         Left            =   600
         TabIndex        =   7
         Top             =   2040
         Width           =   6615
      End
      Begin OptionButton optIllusType 
         Caption         =   "Show first 25 years (with optional highlighted ages)"
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   12
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Index           =   1
         Left            =   600
         TabIndex        =   1
         Top             =   1080
         Width           =   7095
      End
      Begin OptionButton optIllusType 
         Caption         =   "Show all years to age 95 or lapse year"
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   12
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Index           =   0
         Left            =   600
         TabIndex        =   0
         Top             =   240
         Value           =   -1  'True
         Width           =   6735
      End
      Begin TextBox txtIllusAge 
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   360
         Left            =   3960
         MaxLength       =   2
         TabIndex        =   8
         Top             =   2520
         Visible         =   0   'False
         Width           =   495
      End
      Begin Label lblIllusType 
         Caption         =   "Highlight ages: (past the first 25 years)"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   0
         Left            =   840
         TabIndex        =   13
         Top             =   1560
         Visible         =   0   'False
         Width           =   3015
      End
      Begin Label lblIllusType 
         Caption         =   "Show  to Age:   (Default is Age 95)"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   1
         Left            =   840
         TabIndex        =   12
         Top             =   2640
         Visible         =   0   'False
         Width           =   2895
      End
   End
   Begin SSCommand cmdExit 
      BevelWidth      =   4
      Caption         =   "E&XIT"
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   735
      Left            =   4800
      TabIndex        =   10
      Top             =   3840
      Width           =   1815
   End
   Begin SSCommand cmdOK 
      BevelWidth      =   4
      Caption         =   "&OK"
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   735
      Left            =   2400
      TabIndex        =   9
      Top             =   3840
      Width           =   1695
   End
End

Sub cmdExit_Click ()

    LSClient(SpouseClient).IllusType = 0
    Unload frmLSILLTYP

End Sub

Sub cmdOK_Click ()
    
    Unload frmLSILLTYP
    frmLIFEILL.Show 0

End Sub

Sub Form_Load ()
    
    LSClient(SpouseClient).IllusType = 195
    lblIllusType(0).Visible = False
    lblIllusType(1).Visible = False
    txtIllusAge.Visible = False
    txtIllusHighAge(0).Visible = False
    txtIllusHighAge(1).Visible = False
    txtIllusHighAge(2).Visible = False
    txtIllusHighAge(3).Visible = False
    txtIllusHighAge(4).Visible = False

End Sub

Sub Form_Unload (Cancel As Integer)

    Set frmLSILLTYP = Nothing

End Sub

Sub optIllusType_Click (Index As Integer)

    Select Case Index
       Case 1
          LSClient(SpouseClient).IllusType = 200 + LSClient(SpouseClient).IssueAge + 25
          lblIllusType(0).Visible = True
          lblIllusType(1).Visible = False
          txtIllusAge.Visible = False
          txtIllusHighAge(0).Visible = True
          txtIllusHighAge(1).Visible = True
          txtIllusHighAge(2).Visible = True
          txtIllusHighAge(3).Visible = True
          txtIllusHighAge(4).Visible = True
       Case 2
          LSClient(SpouseClient).IllusType = 395
          lblIllusType(0).Visible = False
          lblIllusType(1).Visible = True
          txtIllusAge.Visible = True
          txtIllusHighAge(0).Visible = False
          txtIllusHighAge(1).Visible = False
          txtIllusHighAge(2).Visible = False
          txtIllusHighAge(3).Visible = False
          txtIllusHighAge(4).Visible = False
       Case Else
          LSClient(SpouseClient).IllusType = 195
          lblIllusType(0).Visible = False
          lblIllusType(1).Visible = False
          txtIllusAge.Visible = False
          txtIllusHighAge(0).Visible = False
          txtIllusHighAge(1).Visible = False
          txtIllusHighAge(2).Visible = False
          txtIllusHighAge(3).Visible = False
          txtIllusHighAge(4).Visible = False
    End Select

End Sub

Sub optIllusType_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_HOME And Shift = 4 Then optIllusType(0).SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdExit.SetFocus
    If KeyCode = KEY_RETURN Then
       cmdOK.SetFocus
    End If
    If KeyCode = KEY_UP Then
       If Index - 1 > 0 Then
          optIllusType(Index - 1).SetFocus
       Else
          optIllusType(2).SetFocus
       End If
    End If
    If KeyCode = KEY_DOWN Then
       If Index + 1 < 3 Then
          optIllusType(Index + 1).SetFocus
       Else
          optIllusType(0).SetFocus
       End If
    End If
   'If KeyCode = KEY_F1 Then filemsg msgfile$, 1
    
End Sub

Sub txtIllusAge_GotFocus ()
    
    clipboard.Clear
    clipboard.SetText Trim$(txtIllusAge.Text)
    txtIllusAge.Text = ""
    txtIllusAge.Text = clipboard.GetText()
    txtIllusAge.ForeColor = &H80000005
    txtIllusAge.BackColor = &H80000008

End Sub

Sub txtIllusAge_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_HOME And Shift = 4 Then optIllusType(0).SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdExit.SetFocus
    If KeyCode = KEY_DOWN Then optIllusType(0).SetFocus
    If KeyCode = KEY_RETURN Then cmdOK.SetFocus
    If KeyCode = KEY_UP Then optIllusType(2).SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtIllusAge.Text = clipboard.GetText()
       cmdOK.SetFocus
    End If
   'If KeyCode = KEY_F1 Then filemsg msgfile$, 1

End Sub

Sub txtIllusAge_KeyPress (KeyAscii As Integer)

    If KeyAscii < 48 Or KeyAscii > 57 Then
       KeyAscii = 0
    Else
       If txtIllusAge.Text = clipboard.GetText() Then
          txtIllusAge.Text = ""
       End If
    End If

End Sub

Sub txtIllusAge_LostFocus ()

    txtIllusAge.ForeColor = &H80000008
    txtIllusAge.BackColor = &H80000005
    TempAge% = LSClient(SpouseClient).IssueAge
    If txtIllusAge.Text = "" Or Val(txtIllusAge.Text) = 0 Or Val(txtIllusAge.Text) > 95 Then
       txtIllusAge.Text = "95"
    End If
    If TempAge% + 10 > Val(txtIllusAge.Text) Then
       msgline$ = "Illustrate age must be at least 10 years greater than client issue age of " & Str$(LSClient(SpouseClient).IssueAge)
       MsgBox msgline$
       txtIllusAge.Text = ""
       txtIllusAge.SetFocus
       Exit Sub
    End If
    LSClient(SpouseClient).IllusType = 300 + Val(txtIllusAge.Text)
    

End Sub

Sub txtIllusHighAge_GotFocus (Index As Integer)

    clipboard.Clear
    clipboard.SetText Trim$(txtIllusHighAge(Index).Text)
    txtIllusHighAge(Index).Text = ""
    txtIllusHighAge(Index).Text = clipboard.GetText()
    txtIllusHighAge(Index).ForeColor = &H80000005
    txtIllusHighAge(Index).BackColor = &H80000008

End Sub

Sub txtIllusHighAge_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_HOME And Shift = 4 Then optIllusType(0).SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdExit.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtIllusHighAge(Index).Text = clipboard.GetText()
    End If
    If KeyCode = KEY_DOWN Then optIllusType(2).SetFocus
    If KeyCode = KEY_RETURN Or KeyCode = KEY_ESCAPE Then
       If Index + 1 < 5 Then
          txtIllusHighAge(Index + 1).SetFocus
       Else
          cmdOK.SetFocus
       End If
    End If
    If KeyCode = KEY_UP Then optIllusType(0).SetFocus
   'If KeyCode = KEY_F1 Then filemsg msgfile$, 1

End Sub

Sub txtIllusHighAge_KeyPress (Index As Integer, KeyAscii As Integer)

    If KeyAscii < 48 Or KeyAscii > 57 Then
       KeyAscii = 0
    Else
       If txtIllusHighAge(Index).Text = clipboard.GetText() Then
          txtIllusHighAge(Index).Text = ""
       End If
    End If

End Sub

Sub txtIllusHighAge_LostFocus (Index As Integer)

    Dim HighLight As Integer
    Dim TempAge As Integer

    txtIllusHighAge(Index).ForeColor = &H80000008
    txtIllusHighAge(Index).BackColor = &H80000005
    TempAge = LSClient(SpouseClient).IssueAge
    HighLight = Val(txtIllusHighAge(Index).Text)
    If HighLight = 0 Then
       If Index = 0 Then LSClient(SpouseClient).IllusType = 200 + TempAge + 25
       cmdOK.SetFocus
       Exit Sub
    End If
    If TempAge + 25 > HighLight Then
       msgline$ = "Highlight age must be at least 25 years greater than client issue age of " & Str$(LSClient(SpouseClient).IssueAge)
       MsgBox msgline$
       txtIllusHighAge(Index).Text = ""
       txtIllusHighAge(Index).SetFocus
       Exit Sub
    End If
    If Index > 0 Then
       If Val(txtIllusHighAge(Index).Text) <= LSClient(SpouseClient).HighAges(Index - 1) Then
          msgline$ = "Highlight age must be greater than previous Highlight Age"
          MsgBox msgline$
          txtIllusHighAge(Index).Text = ""
          txtIllusHighAge(Index).SetFocus
          Exit Sub
       End If
    End If
    LSClient(SpouseClient).IllusType = 299   'highlight indicator
    LSClient(SpouseClient).HighAges(Index) = HighLight
                                                     
End Sub

