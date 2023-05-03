VERSION 2.00
Begin Form frmSvClient 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Save Client Information"
   ClientHeight    =   3720
   ClientLeft      =   2145
   ClientTop       =   3255
   ClientWidth     =   7095
   ControlBox      =   0   'False
   Height          =   4200
   Left            =   2085
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   3720
   ScaleWidth      =   7095
   Top             =   2835
   Width           =   7215
   Begin Frame fraClientSave 
      Height          =   3015
      Left            =   240
      TabIndex        =   4
      Top             =   600
      Width           =   6615
      Begin SSCommand cmdClientSave 
         BevelWidth      =   4
         Caption         =   "e&Xit"
         ForeColor       =   &H00000000&
         Height          =   495
         Index           =   3
         Left            =   240
         TabIndex        =   3
         Top             =   2400
         Width           =   1095
      End
      Begin SSCommand cmdClientSave 
         BevelWidth      =   4
         Caption         =   "&Partial"
         ForeColor       =   &H00000000&
         Height          =   495
         Index           =   2
         Left            =   240
         TabIndex        =   2
         Top             =   1680
         Width           =   1095
      End
      Begin SSCommand cmdClientSave 
         BevelWidth      =   4
         Caption         =   "&No"
         ForeColor       =   &H00000000&
         Height          =   495
         Index           =   1
         Left            =   240
         TabIndex        =   1
         Top             =   960
         Width           =   1095
      End
      Begin SSCommand cmdClientSave 
         BevelWidth      =   4
         Caption         =   "&Yes"
         Height          =   495
         Index           =   0
         Left            =   240
         TabIndex        =   0
         Top             =   240
         Width           =   1095
      End
      Begin Label lblClientSave 
         Caption         =   "Return to current display with current client"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   4
         Left            =   1800
         TabIndex        =   9
         Top             =   2520
         Width           =   3855
      End
      Begin Label lblClientSave 
         Caption         =   "Save client, but do not save birthdate"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   3
         Left            =   1800
         TabIndex        =   8
         Top             =   1800
         Width           =   4095
      End
      Begin Label lblClientSave 
         Caption         =   "No, do not save any Client information"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   2
         Left            =   1800
         TabIndex        =   7
         Top             =   1080
         Width           =   3975
      End
      Begin Label lblClientSave 
         Caption         =   "Yes, Save all client information "
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   1
         Left            =   1800
         TabIndex        =   6
         Top             =   360
         Width           =   2895
      End
   End
   Begin Label lblClientSave 
      Alignment       =   2  'Center
      Caption         =   "(Press a button below, or press the ""Y"",""N"", ""P"" or ""X"" keys on the keyboard)"
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   8.25
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   255
      Index           =   5
      Left            =   240
      TabIndex        =   10
      Top             =   360
      Width           =   6615
   End
   Begin Label lblClientSave 
      Alignment       =   2  'Center
      Caption         =   "Do you wish to save Client information before continuing?"
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   255
      Index           =   0
      Left            =   240
      TabIndex        =   5
      Top             =   120
      Width           =   6615
   End
End

Sub cmdClientSave_Click (Index As Integer)

    ClientSave_ReturnCode = Index
    Unload frmSvClient
 
End Sub

Sub cmdClientSave_GotFocus (Index As Integer)

    cmdClientSave(Index).Font3D = 3
    cmdClientSave(Index).FontUnderline = True

End Sub

Sub cmdClientSave_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)

    Select Case KeyCode
       Case 89, 121      '  Yes
          Index = 0
       Case 78, 110      '  No
          Index = 1
       Case 80, 112      '  Partial
          Index = 2
       Case 88, 119      '  eXit
          Index = 3
       Case Else
          KeyCode = 0
    End Select

    If KeyCode <> 0 Then
       Call cmdClientSave_GotFocus(Index)
       Call cmdClientSave_Click(Index)
    End If

End Sub

Sub cmdClientSave_LostFocus (Index As Integer)

    cmdClientSave(Index).Font3D = 0
    cmdClientSave(Index).FontUnderline = False

End Sub

Sub Form_Unload (Cancel As Integer)

    Set frmSvClient = Nothing

End Sub

