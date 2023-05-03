VERSION 2.00
Begin Form frmClntList 
   Caption         =   "Client List"
   ClientHeight    =   7185
   ClientLeft      =   1140
   ClientTop       =   420
   ClientWidth     =   9675
   Height          =   7590
   Left            =   1080
   LinkTopic       =   "Form1"
   ScaleHeight     =   7185
   ScaleWidth      =   9675
   Top             =   75
   Width           =   9795
   Begin SSCommand cmdDelete 
      BevelWidth      =   4
      Caption         =   "&DELETE"
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      ForeColor       =   &H00000000&
      Height          =   735
      Left            =   7320
      TabIndex        =   5
      Top             =   2520
      Width           =   1935
   End
   Begin ListBox List1 
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   5310
      HelpContextID   =   20
      Left            =   360
      Sorted          =   -1  'True
      TabIndex        =   2
      Top             =   1440
      Width           =   6615
   End
   Begin SSPanel Panel3D1 
      BevelInner      =   1  'Inset
      BevelOuter      =   1  'Inset
      BevelWidth      =   4
      Caption         =   "Highlight a Client with the mouse or arrow keys and press the ""SELECT"" button (or doubleclick the highlighted Client) to place that Client's information into the current American Republic Life display."
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   1095
      Left            =   360
      TabIndex        =   0
      Top             =   0
      Width           =   9015
   End
   Begin SSCommand cmdHelp 
      BevelWidth      =   4
      Caption         =   "&HELP"
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      ForeColor       =   &H00000000&
      Height          =   735
      Left            =   7320
      TabIndex        =   4
      Top             =   4680
      Width           =   1935
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
      ForeColor       =   &H00000000&
      Height          =   735
      Left            =   7320
      TabIndex        =   3
      Top             =   3600
      Width           =   1935
   End
   Begin SSCommand cmdSelect 
      BevelWidth      =   4
      Caption         =   "&SELECT"
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      ForeColor       =   &H00000000&
      Height          =   735
      Left            =   7320
      TabIndex        =   1
      Top             =   1440
      Width           =   1935
   End
End

Sub cmdDelete_Click ()

    Dim TempName As String

    frmClntList.MousePointer = 11
    If cmdDelete.Caption = "&DELETE" Then
       TempName = frmClntList.List1.Text
       j = InStr(1, TempName, "#")
       If j <> 0 Then
          j = j + 1
          recnum% = Val(Mid$(TempName, j, Len(TempName)))
       End If
       ClientInfo.ClientNumber = recnum%
       If recnum% <> 0 Then
          client_data = True
          k = Client_File(3, recnum%)
       End If
    Else
       recnum% = 0
       client_data = True
       k = Client_File(4, recnum%)
    End If
    frmClntList.MousePointer = 0
    Unload frmClntList
    Call Refresh_ClntList
    frmClntList.Show

End Sub

Sub cmdExit_Click ()
 
    Call ClearClient
    Unload frmClntList
    frmLifeSv.Show

End Sub

Sub cmdHelp_Click ()

    i = WinHelp(frmClntList.hWnd, "ls3\lifesav3.hlp" + Chr$(0), 1&, 1&)

End Sub

Sub cmdSelect_Click ()

    Dim TempName As String

    frmClntList.MousePointer = 11
    Call ClearClient
    client_data = False
    TempName = frmClntList.List1.Text
    j = InStr(1, TempName, "#")
    If j <> 0 Then
       j = j + 1
       recnum% = Val(Mid$(TempName, j, Len(TempName)))
    End If
    ClientInfo.ClientNumber = recnum%
    If recnum% <> 0 Then
       client_data = True
       k = Client_File(1, recnum%)
    Else
       Call ClearClient
    End If
    frmClntList.MousePointer = 0
    CallFromMain = "NO"
    Unload frmClntList
    frmLifeSv.Show

End Sub

Sub cmdSelect_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then Call cmdSelect_Click

End Sub

Sub Form_Unload (Cancel As Integer)

    Set frmClntList = Nothing

End Sub

Sub List1_DblClick ()

    If List1.ListIndex >= 0 Then Call cmdSelect_Click

End Sub

Sub List1_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN And List1.ListIndex >= 0 Then Call cmdSelect_Click

End Sub

Sub Panel3D1_DblClick ()

    If cmdDelete.Caption = "&DELETE" Then
       cmdDelete.Caption = "UN&DELETE"
    Else
       cmdDelete.Caption = "&DELETE"
    End If

End Sub

