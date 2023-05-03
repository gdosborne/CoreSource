VERSION 2.00
Begin Form frmClntList 
   Caption         =   "Client List"
   ClientHeight    =   5715
   ClientLeft      =   1770
   ClientTop       =   420
   ClientWidth     =   8310
   Height          =   6120
   Left            =   1710
   LinkTopic       =   "Form1"
   ScaleHeight     =   5715
   ScaleWidth      =   8310
   Top             =   75
   Width           =   8430
   Begin SSCommand cmdDelete 
      BevelWidth      =   4
      Caption         =   "&DELETE"
      ForeColor       =   &H00000000&
      Height          =   615
      Left            =   6240
      TabIndex        =   5
      Top             =   1680
      Width           =   1575
   End
   Begin ListBox List1 
      Height          =   4710
      HelpContextID   =   20
      Left            =   480
      Sorted          =   -1  'True
      TabIndex        =   2
      Top             =   840
      Width           =   5640
   End
   Begin SSPanel Panel3D1 
      BevelInner      =   1  'Inset
      BevelOuter      =   1  'Inset
      BevelWidth      =   4
      Caption         =   "Highlight a Client with the mouse or arrow keys and press the ""SELECT"" button (or doubleclick the highlighted Client) to place that Client's information into the current American Republic Life display."
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   8.25
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   735
      Left            =   480
      TabIndex        =   0
      Top             =   0
      Width           =   7335
   End
   Begin SSCommand cmdHelp 
      BevelWidth      =   4
      Caption         =   "&HELP"
      ForeColor       =   &H00000000&
      Height          =   615
      Left            =   6240
      TabIndex        =   4
      Top             =   3360
      Width           =   1575
   End
   Begin SSCommand cmdExit 
      BevelWidth      =   4
      Caption         =   "E&XIT"
      ForeColor       =   &H00000000&
      Height          =   615
      Left            =   6240
      TabIndex        =   3
      Top             =   2520
      Width           =   1575
   End
   Begin SSCommand cmdSelect 
      BevelWidth      =   4
      Caption         =   "&SELECT"
      ForeColor       =   &H00000000&
      Height          =   615
      Left            =   6240
      TabIndex        =   1
      Top             =   840
      Width           =   1575
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

