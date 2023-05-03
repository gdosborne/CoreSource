VERSION 2.00
Begin Form frmCLLIST 
   Caption         =   "Client List"
   ClientHeight    =   5715
   ClientLeft      =   1185
   ClientTop       =   420
   ClientWidth     =   8310
   Height          =   6195
   Left            =   1125
   LinkTopic       =   "Form1"
   ScaleHeight     =   5715
   ScaleWidth      =   8310
   Top             =   0
   Width           =   8430
   Begin SSCommand KeyHelp 
      Caption         =   "&HELP"
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   615
      Left            =   6240
      TabIndex        =   4
      Top             =   2520
      Width           =   1575
   End
   Begin SSCommand KeyExit 
      Caption         =   "E&XIT"
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   615
      Left            =   6240
      TabIndex        =   3
      Top             =   1680
      Width           =   1575
   End
   Begin ListBox List1 
      BackColor       =   &H00FFFFFF&
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      ForeColor       =   &H00000000&
      Height          =   4830
      HelpContextID   =   20
      Left            =   480
      Sorted          =   -1  'True
      TabIndex        =   2
      Top             =   840
      Width           =   5640
   End
   Begin SSCommand KeySelect 
      Caption         =   "&SELECT"
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   615
      Left            =   6240
      TabIndex        =   1
      Top             =   840
      Width           =   1575
   End
   Begin SSPanel Panel3D1 
      BevelInner      =   1  'Inset
      BevelOuter      =   1  'Inset
      BevelWidth      =   4
      Caption         =   "Select Client with arrow key or mouse, then enter or select action."
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   735
      Left            =   480
      TabIndex        =   0
      Top             =   0
      Width           =   7335
   End
End

Sub Form_Unload (Cancel As Integer)

    Set frmCLLIST = Nothing

End Sub

Sub KeyExit_Click ()
 
    Call ClearClient
    Unload frmCLLIST
    frmLIFESV.Show

End Sub

Sub KeySelect_Click ()

    Dim TempName As String

    frmCLLIST.MousePointer = 11
    Call ClearClient
    client_data = False
    TempName = frmCLLIST.List1.Text
    r% = InStr(1, TempName, "#")
    If r% <> 0 Then
       r% = r% + 1
       recnum% = Val(Mid$(TempName, r%, Len(TempName)))
    End If
    ClientInfo.ClientNumber = recnum%
    If recnum% <> 0 Then
       client_data = True
       s% = client_file(1, recnum%)
    Else
       Call ClearClient
    End If
    frmCLLIST.MousePointer = 0
    Unload frmCLLIST
    frmLIFESV.Show
    
End Sub

Sub KeySelect_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then Call KeySelect_Click

End Sub

Sub List1_DblClick ()

    If List1.ListIndex >= 0 Then Call KeySelect_Click

End Sub

Sub List1_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN And List1.ListIndex >= 0 Then Call KeySelect_Click

End Sub

