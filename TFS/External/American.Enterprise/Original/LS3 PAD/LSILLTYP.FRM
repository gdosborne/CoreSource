VERSION 2.00
Begin Form frmLSILLTYP 
   Caption         =   "LifeSavings Illustration - Ledger Years to Illustrate"
   ClientHeight    =   3165
   ClientLeft      =   825
   ClientTop       =   855
   ClientWidth     =   10350
   ControlBox      =   0   'False
   Height          =   3570
   Left            =   765
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   3165
   ScaleWidth      =   10350
   Top             =   510
   Width           =   10470
   Begin Frame fraIllusType 
      Height          =   1935
      Left            =   120
      TabIndex        =   4
      Top             =   120
      Width           =   10095
      Begin OptionButton optIllusType 
         Caption         =   "Show first 25 years, then every 5 years until age 95 or lapse year "
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   12
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Index           =   2
         Left            =   600
         TabIndex        =   1
         Top             =   1080
         Width           =   8175
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
         Top             =   360
         Value           =   -1  'True
         Width           =   4935
      End
   End
   Begin SSCommand cmdExit 
      BevelWidth      =   4
      Caption         =   "E&XIT"
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   12
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   855
      Left            =   5400
      TabIndex        =   3
      Top             =   2160
      Width           =   1815
   End
   Begin SSCommand cmdOK 
      BevelWidth      =   4
      Caption         =   "&OK"
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   12
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   855
      Left            =   3120
      TabIndex        =   2
      Top             =   2160
      Width           =   1815
   End
End

Sub cmdExit_Click ()

    LSClient(SpouseClient).IllusType = 0
    Unload frmLSIllTyp

End Sub

Sub cmdOK_Click ()
    
    frmLSIllTyp.MousePointer = 11
    Unload frmLSIllTyp
    frmvsPrint.Show 0

End Sub

Sub Form_Load ()
    
    LSClient(SpouseClient).IllusType = 195    'Default; all years to age 95

End Sub

Sub Form_Unload (Cancel As Integer)

    Set frmLSIllTyp = Nothing

End Sub

Sub optIllusType_Click (Index As Integer)

    Select Case Index
       Case 2
          LSClient(SpouseClient).IllusType = 395
       Case Else
          LSClient(SpouseClient).IllusType = 195
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
    
End Sub

