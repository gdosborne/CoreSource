VERSION 2.00
Begin Form frmSVCLIENT 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Save Client Information"
   ClientHeight    =   2850
   ClientLeft      =   2145
   ClientTop       =   3885
   ClientWidth     =   5505
   ControlBox      =   0   'False
   Height          =   3255
   Left            =   2085
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   2850
   ScaleWidth      =   5505
   Top             =   3540
   Width           =   5625
   Begin Frame frmClientSave 
      Height          =   2175
      Left            =   240
      TabIndex        =   4
      Top             =   600
      Width           =   5055
      Begin SSCommand cmdClientSave 
         BevelWidth      =   4
         Caption         =   "eXit"
         Font3D          =   0  'None
         ForeColor       =   &H00000000&
         Height          =   375
         Index           =   3
         Left            =   240
         TabIndex        =   3
         Top             =   1680
         Width           =   975
      End
      Begin SSCommand cmdClientSave 
         BevelWidth      =   4
         Caption         =   "Partial"
         Font3D          =   0  'None
         ForeColor       =   &H00000000&
         Height          =   375
         Index           =   2
         Left            =   240
         TabIndex        =   2
         Top             =   1200
         Width           =   975
      End
      Begin SSCommand cmdClientSave 
         BevelWidth      =   4
         Caption         =   "No"
         Font3D          =   0  'None
         ForeColor       =   &H00000000&
         Height          =   375
         Index           =   1
         Left            =   240
         TabIndex        =   1
         Top             =   720
         Width           =   975
      End
      Begin SSCommand cmdClientSave 
         BevelWidth      =   4
         Caption         =   "Yes"
         Font3D          =   3  'Inset w/light shading
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   -1  'True
         ForeColor       =   &H00000000&
         Height          =   375
         Index           =   0
         Left            =   240
         TabIndex        =   0
         Top             =   240
         Width           =   975
      End
      Begin Label lblClientSave 
         Caption         =   "Return to current display"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   4
         Left            =   1560
         TabIndex        =   9
         Top             =   1800
         Width           =   3015
      End
      Begin Label lblClientSave 
         Caption         =   "Save, but do not replace birthdate"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   3
         Left            =   1560
         TabIndex        =   8
         Top             =   1320
         Width           =   3015
      End
      Begin Label lblClientSave 
         Caption         =   "Do not save Client information"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   2
         Left            =   1560
         TabIndex        =   7
         Top             =   840
         Width           =   2775
      End
      Begin Label lblClientSave 
         Caption         =   "Save Client information "
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   1
         Left            =   1560
         TabIndex        =   6
         Top             =   360
         Width           =   2175
      End
   End
   Begin Label lblClientSave 
      Caption         =   "(Click a button below, or type  Y,  N,  P,  or  X  to make your selection)   "
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
      Width           =   5055
   End
   Begin Label lblClientSave 
      Alignment       =   2  'Center
      Caption         =   "Do you wish to save Client information before continuing?"
      Height          =   255
      Index           =   0
      Left            =   240
      TabIndex        =   5
      Top             =   120
      Width           =   5055
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
       Case 89, 121      'Yes
          Index = 0
       Case 78, 110      'No
          Index = 1
       Case 80, 112      'Partial
          Index = 2
       Case 88, 119      'eXit
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

