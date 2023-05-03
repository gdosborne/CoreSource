VERSION 2.00
Begin Form frmLSSPOUSE 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Life Savings: Spouse as Client Illustration Parameters"
   ClientHeight    =   8025
   ClientLeft      =   60
   ClientTop       =   420
   ClientWidth     =   11880
   ControlBox      =   0   'False
   Height          =   8430
   Left            =   0
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   435
   ScaleMode       =   0  'User
   ScaleWidth      =   2412.183
   Top             =   75
   Width           =   12000
   Begin MaskEdBox txtSpouseTaxID 
      Enabled         =   0   'False
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   360
      Left            =   9600
      Mask            =   "###-##-####"
      MaxLength       =   11
      PromptChar      =   "_"
      TabIndex        =   48
      Top             =   1560
      Visible         =   0   'False
      Width           =   1335
   End
   Begin TextBox txtSpouseAge 
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   360
      Left            =   1800
      MaxLength       =   2
      TabIndex        =   3
      Top             =   600
      Width           =   375
   End
   Begin TextBox txtSpouseCurrentInt 
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   360
      Left            =   5640
      MaxLength       =   6
      TabIndex        =   8
      Top             =   1560
      Width           =   975
   End
   Begin TextBox txtSpouseState 
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   360
      Left            =   11160
      MaxLength       =   2
      TabIndex        =   9
      Top             =   1560
      Width           =   495
   End
   Begin Frame fraSpouseExtras 
      Caption         =   "Illustrate Using:"
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   12
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   1455
      Index           =   1
      Left            =   120
      TabIndex        =   38
      Top             =   3480
      Width           =   11535
      Begin SSCommand cmdFuture 
         BevelWidth      =   4
         Caption         =   "&FUTURE Changes"
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         ForeColor       =   &H00000000&
         Height          =   495
         Left            =   8880
         TabIndex        =   21
         Top             =   840
         Width           =   2415
      End
      Begin SSCommand cmdIllustrate 
         BevelWidth      =   4
         Caption         =   "&ILLUSTRATE"
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         ForeColor       =   &H00000000&
         Height          =   495
         Left            =   3000
         TabIndex        =   19
         Top             =   840
         Width           =   2655
      End
      Begin SSCommand cmdPremium 
         BevelWidth      =   4
         Caption         =   "&PREMIUM CALC"
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         ForeColor       =   &H00000000&
         Height          =   495
         Left            =   5880
         TabIndex        =   20
         Top             =   840
         Width           =   2775
      End
      Begin SSOption optSpousePremiumChoice 
         Caption         =   "Target Premium"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   240
         Index           =   1
         Left            =   120
         TabIndex        =   17
         TabStop         =   0   'False
         Top             =   720
         Width           =   2535
      End
      Begin SSOption optSpousePremiumChoice 
         Caption         =   "Minimum Premium"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   240
         Index           =   2
         Left            =   120
         TabIndex        =   18
         TabStop         =   0   'False
         Top             =   1080
         Width           =   2535
      End
      Begin SSOption optSpousePremiumChoice 
         Caption         =   "Guideline Premium"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   240
         Index           =   0
         Left            =   120
         TabIndex        =   16
         Top             =   360
         Value           =   -1  'True
         Width           =   2535
      End
      Begin Label lblIll3 
         Alignment       =   2  'Center
         Caption         =   "Display and Make Policy Changes for Future Years Before Illustrating"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   615
         Left            =   8760
         TabIndex        =   41
         Top             =   240
         Width           =   2655
      End
      Begin Label lblIll2 
         Alignment       =   2  'Center
         Caption         =   "Display/Change Planned Premium, Mode, Lump Sum Payments or Target Cash Value Before Illustrating"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   615
         Left            =   5880
         TabIndex        =   40
         Top             =   240
         Width           =   2775
      End
      Begin Label lblIll1 
         Alignment       =   2  'Center
         Caption         =   """Quick"" Illustration Using Current Options (above) and Annual Premium (selected at left)"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   615
         Left            =   3000
         TabIndex        =   39
         Top             =   240
         Width           =   2655
      End
   End
   Begin TextBox txtSpouseLastName 
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   360
      Left            =   7080
      MaxLength       =   20
      TabIndex        =   2
      Top             =   120
      Width           =   4575
   End
   Begin TextBox txtSpouseMidInit 
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   360
      Left            =   6720
      MaxLength       =   1
      TabIndex        =   1
      Top             =   120
      Width           =   255
   End
   Begin Frame fraSpouseExtras 
      Caption         =   "Additional Features and Riders for Spouse"
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   12
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   1455
      Index           =   0
      Left            =   120
      TabIndex        =   33
      Top             =   2040
      Width           =   11535
      Begin TextBox txtSpouseFlatRate 
         Enabled         =   0   'False
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   360
         Left            =   8040
         MaxLength       =   10
         TabIndex        =   13
         Top             =   960
         Width           =   975
      End
      Begin ComboBox cboSpouseRiderYrAgeEnd 
         Enabled         =   0   'False
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   360
         Left            =   10200
         Style           =   2  'Dropdown List
         TabIndex        =   15
         Top             =   960
         Width           =   975
      End
      Begin ComboBox cboSpouseWPD 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   360
         Left            =   960
         Style           =   2  'Dropdown List
         TabIndex        =   10
         Top             =   960
         Width           =   1455
      End
      Begin ComboBox cboSpouseGPO 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   360
         Left            =   3360
         Style           =   2  'Dropdown List
         TabIndex        =   11
         Top             =   960
         Width           =   1575
      End
      Begin ComboBox cboSpouseSubStd 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   360
         Left            =   5760
         Style           =   2  'Dropdown List
         TabIndex        =   12
         Top             =   960
         Width           =   1335
      End
      Begin MaskEdBox txtSpouseRiderYearEnd 
         Enabled         =   0   'False
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   360
         Left            =   9840
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   "_"
         TabIndex        =   14
         Top             =   960
         Width           =   375
      End
      Begin Label lblSpouseRider 
         Alignment       =   2  'Center
         Caption         =   "Years or To Age for Flat Extra Premium"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Index           =   2
         Left            =   9600
         TabIndex        =   47
         Top             =   480
         Width           =   1695
      End
      Begin Label lblSpouseRider 
         Alignment       =   2  'Center
         Caption         =   "Flat Extra Premium per $1000 of Death Benefit"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Index           =   1
         Left            =   7440
         TabIndex        =   46
         Top             =   495
         Width           =   2055
      End
      Begin Label lblWPD 
         Alignment       =   2  'Center
         Caption         =   "Disability Waiver of Cost of Insurance Rider"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   495
         Left            =   840
         TabIndex        =   36
         Top             =   360
         Width           =   1695
      End
      Begin Label lblGPO 
         Alignment       =   2  'Center
         Caption         =   "Guaranteed Added Death Benefit Rider"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   495
         Left            =   3360
         TabIndex        =   35
         Top             =   360
         Width           =   1575
      End
      Begin Label lblSpouseRider 
         Alignment       =   2  'Center
         Caption         =   "- - - - - - - - -  Substandard Table Ratings and Flat Extra Premium - - - - - - - - - "
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   0
         Left            =   5760
         TabIndex        =   37
         Top             =   255
         Width           =   5535
      End
      Begin Label lblSpouseSubStd 
         Alignment       =   2  'Center
         Caption         =   "Substandard Tables"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Left            =   5760
         TabIndex        =   34
         Top             =   495
         Width           =   1335
      End
   End
   Begin TextBox txtSpouseSex 
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   360
      Left            =   11280
      MaxLength       =   1
      TabIndex        =   5
      Top             =   600
      Width           =   375
   End
   Begin ComboBox cboSpousePlan 
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   360
      Left            =   4680
      Style           =   2  'Dropdown List
      TabIndex        =   6
      Top             =   1080
      Width           =   1935
   End
   Begin TextBox txtSpouseFirstName 
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   360
      Left            =   1800
      MaxLength       =   20
      TabIndex        =   0
      Top             =   120
      Width           =   4815
   End
   Begin Frame fraSpouseCommands 
      Height          =   2775
      Left            =   2040
      TabIndex        =   42
      Top             =   5160
      Width           =   7815
      Begin SSCommand cmdCancel 
         BevelWidth      =   4
         Caption         =   "E&XIT"
         ForeColor       =   &H00000000&
         Height          =   615
         Left            =   240
         TabIndex        =   23
         Top             =   1080
         Width           =   1095
      End
      Begin SSCommand cmdSave 
         BevelWidth      =   4
         Caption         =   "&SAVE"
         ForeColor       =   &H00000000&
         Height          =   615
         Left            =   240
         TabIndex        =   24
         Top             =   1920
         Width           =   1095
      End
      Begin SSCommand cmdOK 
         BevelWidth      =   4
         Caption         =   "&OK"
         ForeColor       =   &H00000000&
         Height          =   615
         Left            =   240
         TabIndex        =   22
         Top             =   240
         Width           =   1095
      End
      Begin Label lblSpouseCommand 
         Caption         =   "Return to Principal Insured Parameter Display; ignore all changes"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   2
         Left            =   1560
         TabIndex        =   45
         Top             =   1320
         Width           =   6135
      End
      Begin Label lblSpouseCommand 
         Caption         =   "Save data on separate New Client and return to Principal Insured"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   1
         Left            =   1560
         TabIndex        =   44
         Top             =   2160
         Width           =   6135
      End
      Begin Label lblSpouseCommand 
         Caption         =   "Retain Data and Return to Principal Insured Parameter Display"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   0
         Left            =   1560
         TabIndex        =   43
         Top             =   480
         Width           =   6135
      End
   End
   Begin MaskEdBox txtSpouseFace 
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   360
      Left            =   10680
      Mask            =   "#######"
      MaxLength       =   7
      PromptChar      =   "_"
      TabIndex        =   7
      Top             =   1080
      Width           =   975
   End
   Begin MaskEdBox txtSpouseBirth 
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   360
      Left            =   5400
      Mask            =   "##/##/####"
      MaxLength       =   10
      PromptChar      =   "_"
      TabIndex        =   4
      Top             =   600
      Width           =   1215
   End
   Begin Label lblSpouseInt 
      Caption         =   "Current Interest Rate:"
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   255
      Left            =   120
      TabIndex        =   32
      Top             =   1680
      Width           =   2295
   End
   Begin Label lblSpouseState 
      Caption         =   "State of Residence:"
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   255
      Left            =   7080
      TabIndex        =   29
      Top             =   1680
      Width           =   2415
   End
   Begin Label lblSpouseFace 
      Caption         =   "Initial Specified Death Benefit:"
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   255
      Left            =   7080
      TabIndex        =   31
      Top             =   1200
      Width           =   3135
   End
   Begin Label lblSpouseSex 
      Caption         =   "Gender:"
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   255
      Left            =   7080
      TabIndex        =   28
      Top             =   720
      Width           =   855
   End
   Begin Label lblSpousePlan 
      Caption         =   "Tobacco Use:"
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   255
      Left            =   120
      TabIndex        =   30
      Top             =   1200
      Width           =   1815
   End
   Begin Label lblSpouseBirth 
      Caption         =   "or Birthdate:"
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   255
      Left            =   3720
      TabIndex        =   27
      Top             =   720
      Width           =   1455
   End
   Begin Label lblSpouseAge 
      Caption         =   "Spouse Age:"
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   255
      Left            =   120
      TabIndex        =   26
      Top             =   720
      Width           =   1575
   End
   Begin Label lblSpouseName 
      Caption         =   "Spouse Name:"
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   255
      Left            =   120
      TabIndex        =   25
      Top             =   240
      Width           =   1575
   End
End

Sub cboSpouseGPO_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then cboSpouseSubStd.SetFocus

End Sub

Sub cboSpouseGPO_LostFocus ()
    
    If Val(txtSpouseAge.Text) > 39 Then
       If cboSpouseGPO.ListIndex <> 0 Then
          MsgText = "Guaranteed Added Death Benefit Rider not allowed; Spouse must be less than age 40"
          MsgBox MsgText, , "LifeSavings"
          cboSpouseGPO.ListIndex = 0
          Exit Sub
       End If
    End If
    
    If cboSpouseSubStd.ListIndex > 0 Then
       If cboSpouseGPO.ListIndex <> 0 Then
          MsgText = "Guaranteed Added Death Benefit Rider not allowed if Spouse has substandard Table Rating"
          MsgBox MsgText, , "LifeSavings"
          cboSpouseGPO.ListIndex = 0
          Exit Sub
       End If
    End If
    
End Sub

Sub cboSpousePlan_Change ()
    
    SpouseChanged = True

End Sub

Sub cboSpousePlan_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then txtSpouseFace.SetFocus

End Sub

Sub cboSpouseRiderYrAgeEnd_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_RETURN Then
       If optSpousePremiumChoice(0).Value = True Then
          optSpousePremiumChoice(0).SetFocus
       ElseIf optSpousePremiumChoice(1).Value = True Then
          optSpousePremiumChoice(1).SetFocus
       Else
          optSpousePremiumChoice(2).SetFocus
       End If
    End If

End Sub

Sub cboSpouseRiderYrAgeEnd_LostFocus ()
    
    Dim TempAge As Integer

    If Val(txtSpouseFlatRate.Text) = 0 Then Exit Sub
    TempAge = Val(txtSpouseAge.Text)
    If txtSpouseRiderYearEnd.Text = "__" Then
       If cboSpouseRiderYrAgeEnd.ListIndex = 1 Then
          txtSpouseRiderYearEnd.Text = 95
       Else
          txtSpouseRiderYearEnd.Text = 95 - TempAge
       End If
    End If
    Select Case cboSpouseRiderYrAgeEnd.ListIndex
       Case 1
          If Val(txtSpouseRiderYearEnd.Text) <= TempAge Then
             MsgText = "Age for Flat Extra Premium must be greater than Spouse Issue Age"
             MsgBox MsgText, , "LifeSavings"
             txtSpouseRiderYearEnd.SetFocus
             Exit Sub
          End If
       Case Else
          If Val(txtSpouseRiderYearEnd.Text) > 95 - TempAge Then
             MsgText = "Years for Flat Extra Premium cannot be greater than Spouse Age 95"
             MsgBox MsgText, , "LifeSavings"
             txtSpouseRiderYearEnd.SetFocus
             Exit Sub
          End If
    End Select

End Sub

Sub cboSpouseSubStd_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then cmdPremium.SetFocus

End Sub

Sub cboSpouseSubStd_LostFocus ()

    If cboSpouseSubStd.ListIndex > 0 Then
       If cboSpouseWPD.ListIndex <> 0 Or cboSpouseGPO.ListIndex <> 0 Then
          MsgText = "Disability Waiver of Cost of Insurance Rider or Guaranteed Added Death Benefit Rider not allowed on substandard illustration."
          MsgBox MsgText, , "LifeSavings"
       End If
       cboSpouseWPD.ListIndex = 0    'not allowed if substd
       cboSpouseGPO.ListIndex = 0
       'cboSpouseCOLA.ListIndex = 0
    End If

End Sub

Sub cboSpouseWPD_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then cboSpouseGPO.SetFocus

End Sub

Sub cboSpouseWPD_LostFocus ()
    
    If Val(txtSpouseAge.Text) > 55 Then
       If cboSpouseWPD.ListIndex <> 0 Then
          MsgText = "Disability Waiver of Cost of Insurance Rider not allowed; Spouse must be less than age 56."
          MsgBox MsgText, , "LifeSavings"
          cboSpouseWPD.ListIndex = 0
          Exit Sub
       End If
    End If
    
    If Val(txtSpouseAge.Text) < 15 Then
       If cboSpouseWPD.ListIndex <> 0 Then
          MsgText = "Disability Waiver of Cost of Insurance Rider not allowed; Spouse must be greater than age 14."
          MsgBox MsgText, , "LifeSavings"
          cboSpouseWPD.ListIndex = 0
          Exit Sub
       End If
    End If
    
    If cboSpouseSubStd.ListIndex > 0 Then
       If cboSpouseWPD.ListIndex <> 0 Then
          MsgText = "Disability Waiver of Cost of Insurance Rider not allowed if Spouse has substandard Table Rating."
          MsgBox MsgText, , "LifeSavings"
          cboSpouseWPD.ListIndex = 0
          Exit Sub
       End If
    End If
          
End Sub

Sub cmdCancel_Click ()

    Unload frmLSSPOUSE
    SpouseClient = 0
    frmLIFESV.Show 0

End Sub

Sub cmdFuture_Click ()

    Unload frmLSSPOUSE
    frmLSCHANGE.Show 0

End Sub

Sub cmdIllustrate_Click ()

    s = SpouseClient
    Call Set_Spouse_Illus_Parameters

    i = ReadyToIllustrate()
    If i <> 0 Then
       Select Case (i)
          Case 1
             MsgText = "Cannot illustrate until spouse issue age is valid."
          Case 2
             MsgText = "Cannot Illustrate until spouse gender is valid."
          Case 3
             MsgText = "Cannot Illustrate until spouse face amount is valid."
          Case 4
             MsgText = "Cannot Illustrate until spouse face amount is valid for state."
          Case 5
             MsgText = "Cannot Illustrate until spouse interest rate is valid."
          Case Else
       End Select
       MsgBox MsgText, , "LifeSavings"
       Exit Sub
    End If

    frmLSSPOUSE.MousePointer = 11
    frmLSPREM.txtPremCurrentInt.Text = frmLSSPOUSE.txtSpouseCurrentInt.Text
    frmLSPREM.cboPremiumMode.ListIndex = 3  'Annual mode assumed
    frmLSPREM.txtModalPremium.Text = 0
    frmLSPREM.cboYearsPay.Text = Format$(95 - Val(frmLSSPOUSE.txtSpouseAge.Text), "##") & " Years"
    frmLSPREM.txtPremiumLump = ""
    
    Call Set_Premium_Illus_Parameters
    Call Calculate_Initial_Premiums
    
    If frmLSSPOUSE.optSpousePremiumChoice(0).Value = True Then
       frmLSPREM.txtModalPremium.Text = Format$(CalcPrem.GuideAnnual, "000000.00")
    ElseIf frmLSSPOUSE.optSpousePremiumChoice(1).Value = True Then
       frmLSPREM.txtModalPremium.Text = Format$(CalcPrem.TotTarg, "000000.00")
    ElseIf frmLSSPOUSE.optSpousePremiumChoice(2).Value = True Then
       frmLSPREM.txtModalPremium.Text = Format$(CalcPrem.TotMin, "000000.00")
    End If
    
    Call Set_Premium_Illus_Parameters
    Call Calculate_Full_LS_Ledger
    frmLSSPOUSE.MousePointer = 0
    
    frmLSILLTYP.Show 0

End Sub

Sub cmdOK_Click ()

    Call Set_Spouse_Illus_Parameters
    Unload frmLSSPOUSE
    SpouseClient = 0
    frmLIFESV.Show 0

End Sub

Sub cmdPremium_Click ()

    Call Set_Spouse_Illus_Parameters
    Unload frmLSSPOUSE
    frmLSPREM.Show 0

End Sub

Sub cmdSave_Click ()

    Call Set_Spouse_Illus_Parameters
    Call SpouseSave
    Unload frmLSSPOUSE
    frmLIFESV.Show 0

End Sub

Sub Form_Load ()
    
    SpouseClient = 1
    
    If LSClient(0).SexCode = "M" Then txtSpouseSex.Text = "F"
    If LSClient(0).SexCode = "F" Then txtSpouseSex.Text = "M"
    If LSClient(0).CurrIntRate > 0 Then txtSpouseCurrentInt.Text = Format$(LSClient(0).CurrIntRate, "00.000")
    If LSClient(0).State <> "" Then txtSpouseState.Text = LSClient(0).State

    cboSpousePlan.AddItem "No Tobacco Use"
    cboSpousePlan.AddItem "Tobacco User"
    cboSpousePlan.ListIndex = 0
    cboSpouseSubStd.AddItem "None"
    cboSpouseSubStd.AddItem "1.25"
    cboSpouseSubStd.AddItem "1.50"
    cboSpouseSubStd.AddItem "1.75"
    cboSpouseSubStd.AddItem "2.00"
    cboSpouseSubStd.AddItem "2.25"
    cboSpouseSubStd.AddItem "2.50"
    cboSpouseSubStd.AddItem "2.75"
    cboSpouseSubStd.AddItem "3.00"
    cboSpouseSubStd.AddItem "3.75"
    cboSpouseSubStd.AddItem "4.00"
    cboSpouseSubStd.AddItem "4.25"
    cboSpouseSubStd.AddItem "4.50"
    cboSpouseSubStd.AddItem "4.75"
    cboSpouseSubStd.AddItem "5.00"
    cboSpouseSubStd.ListIndex = 0
    cboSpouseRiderYrAgeEnd.AddItem "Year"
    cboSpouseRiderYrAgeEnd.AddItem "Age"
    cboSpouseRiderYrAgeEnd.ListIndex = 0
    cboSpouseGPO.AddItem "None"
    cboSpouseGPO.AddItem "5,000"
    cboSpouseGPO.AddItem "6,000"
    cboSpouseGPO.AddItem "7,000"
    cboSpouseGPO.AddItem "8,000"
    cboSpouseGPO.AddItem "9,000"
    cboSpouseGPO.AddItem "10,000"
    cboSpouseGPO.AddItem "11,000"
    cboSpouseGPO.AddItem "12,000"
    cboSpouseGPO.AddItem "13,000"
    cboSpouseGPO.AddItem "14,000"
    cboSpouseGPO.AddItem "15,000"
    cboSpouseGPO.AddItem "16,000"
    cboSpouseGPO.AddItem "17,000"
    cboSpouseGPO.AddItem "18,000"
    cboSpouseGPO.AddItem "19,000"
    cboSpouseGPO.AddItem "20,000"
    cboSpouseGPO.AddItem "21,000"
    cboSpouseGPO.AddItem "22,000"
    cboSpouseGPO.AddItem "23,000"
    cboSpouseGPO.AddItem "24,000"
    cboSpouseGPO.AddItem "25,000"
    cboSpouseGPO.ListIndex = 0
    'cboSpouseCOLA.AddItem "None"
    'cboSpouseCOLA.AddItem "1%"
    'cboSpouseCOLA.AddItem "2%"
    'cboSpouseCOLA.AddItem "3%"
    'cboSpouseCOLA.AddItem "4%"
    'cboSpouseCOLA.AddItem "5%"
    'cboSpouseCOLA.AddItem "6%"
    'cboSpouseCOLA.AddItem "7%"
    'cboSpouseCOLA.AddItem "8%"
    'cboSpouseCOLA.AddItem "9%"
    'cboSpouseCOLA.AddItem "10%"
    'cboSpouseCOLA.ListIndex = 0
    cboSpouseWPD.AddItem "No"
    cboSpouseWPD.AddItem "Yes"
    cboSpouseWPD.ListIndex = 0

    Call Load_Spouse_As_Client
    
    If ClientInfo.ClientNumber = 0 Then
       lblSpouseCommand(1).Visible = False
       cmdSave.Visible = False
    Else
       lblSpouseCommand(1).Visible = True
       cmdSave.Visible = True
    End If
    AgeChanged = 0

End Sub

Sub Form_Unload (Cancel As Integer)

    Set frmLSSPOUSE = Nothing
    
End Sub

Sub lblSpouseAge_DblClick ()

    AgeChanged = 3

End Sub

Sub txtSpouseAge_Change ()
 
    SpouseChanged = True
    If AgeChanged <> 3 Then AgeChanged = 1

End Sub

Sub txtSpouseAge_GotFocus ()
 
    Clipboard.Clear
    Clipboard.SetText Trim$(txtSpouseAge.Text)
    txtSpouseAge.Text = ""
    txtSpouseAge.Text = Clipboard.GetText()
    txtSpouseAge.ForeColor = &H80000005
    txtSpouseAge.BackColor = &H80000008
    If AgeChanged <> 3 Then AgeChanged = 0

End Sub

Sub txtSpouseAge_KeyDown (KeyCode As Integer, Shift As Integer)
    
    Select Case (KeyCode)
       Case KEY_HOME, KEY_END
          If Shift = 4 Then
             Call txtSpouseAge_LostFocus
             If KeyCode = KEY_HOME Then txtSpouseFirstName.SetFocus
             If KeyCode = KEY_END Then cmdCancel.SetFocus
          End If
       Case KEY_DOWN, KEY_UP, KEY_RETURN, KEY_ESCAPE
          If KeyCode = KEY_RETURN And AgeChanged <> 3 Then AgeChanged = 2
          If KeyCode = KEY_ESCAPE Then
             txtSpouseAge.Text = Clipboard.GetText()
             If AgeChanged <> 3 Then AgeChanged = 2
          End If
          Call txtSpouseAge_LostFocus
          If KeyCode = KEY_DOWN Then cboSpousePlan.SetFocus
          If KeyCode = KEY_UP Then txtSpouseFirstName.SetFocus
    End Select

End Sub

Sub txtSpouseAge_KeyPress (KeyAscii As Integer)

    If KeyAscii < 48 Or KeyAscii > 57 Then
       KeyAscii = 0
    Else
       If txtSpouseAge.Text = Clipboard.GetText() Then
          txtSpouseAge.Text = ""
       End If
    End If

End Sub

Sub txtSpouseAge_KeyUp (KeyCode As Integer, Shift As Integer)

    If (KeyCode > 47 And KeyCode < 58) Or (KeyCode > 95 And KeyCode < 106) Then
       If Len(txtSpouseAge.Text) >= 2 Then
          If AgeChanged <> 3 Then AgeChanged = 2
          Call txtSpouseAge_LostFocus
       End If
    End If

End Sub

Sub txtSpouseAge_LostFocus ()

    Dim TempAge As Integer

    If AgeChanged = 0 Or txtSpouseAge.Text = "" Then GoTo End_AgeProc
    TempAge = Val(txtSpouseAge.Text)
    MsgText = ""
    If TempAge > 80 Then
       MsgText = "Issue age must be less than 81 for a LifeSavings spouse"
    End If
    If TempAge < 15 Then
       MsgText = "Issue age must be at least age 15 for a LifeSavings spouse"
    End If
    If MsgText <> "" Then
       MsgBox MsgText, , "LifeSavings"
       txtSpouseAge.Text = ""
       txtSpouseAge.SetFocus
       Exit Sub
    End If
    
    If AgeChanged <> 3 Then
       If Val(txtSpouseBirth.Text) <> 0 Then
           mm$ = Left$(txtSpouseBirth.Text, 2)
           dd$ = Mid$(txtSpouseBirth.Text, 4, 2)
       Else
           mm$ = Str$(Month(Now))
           dd$ = Str$(Day(Now))
       End If
       If Val(mm$) > Month(Now) Or (Val(mm$) = Month(Now) And Val(dd$) > Day(Now)) Then
           yy$ = Str$(Year(Now) - Val(txtSpouseAge.Text) - 1)
       Else
           yy$ = Str$(Year(Now) - Val(txtSpouseAge.Text))
       End If
       serialdt& = DateSerial(Val(yy$), Val(mm$), Val(dd$))
       txtSpouseBirth.Text = Format$(serialdt&, "MM/DD/YYYY")
    End If

    If TempAge > 36 Then
       cboSpouseGPO.ListIndex = 0
       If TempAge > 59 Then
          cboSpouseWPD.ListIndex = 0
          If TempAge > 63 Then
             'cboSpouseCOLA.ListIndex = 0
          End If
       End If
    End If

End_AgeProc:
    
    If AgeChanged >= 2 Then txtSpouseBirth.SetFocus
    If Val(txtSpouseFace.Text) > 0 Then
       Select Case TempAge
          Case 61 To 70
             If Val(txtSpouseFace.Text) > 0 And Val(txtSpouseFace.Text) < 15000 Then
                txtSpouseFace.Text = "15000__"
             End If
          Case 51 To 60
             If Val(txtSpouseFace.Text) > 0 And Val(txtSpouseFace.Text) < 20000 Then
                txtSpouseFace.Text = "20000__"
             End If
          Case Else
             If Val(txtSpouseFace.Text) > 0 And Val(txtSpouseFace.Text) < 25000 Then
                txtSpouseFace.Text = "25000__"
             End If
       End Select
    End If
    If Val(txtSpouseRiderYearEnd.Text) > 0 Then
       txtSpouseRiderYearEnd.Text = Right$(Str$(95 - Val(txtSpouseAge.Text)), 2)
       cboSpouseRiderYrAgeEnd.ListIndex = 0
    End If
    txtSpouseAge.ForeColor = &H80000008
    txtSpouseAge.BackColor = &H80000005

End Sub

Sub txtSpouseBirth_Change ()
 
    SpouseChanged = True
    BirthChanged = 1

End Sub

Sub txtSpouseBirth_GotFocus ()
 
    Clipboard.Clear
    Clipboard.SetText Trim$(txtSpouseBirth.Text)
    txtSpouseBirth.Text = "__/__/____"
    txtSpouseBirth.Text = Clipboard.GetText()
    txtSpouseBirth.ForeColor = &H80000005
    txtSpouseBirth.BackColor = &H80000008
    BirthChanged = 0

End Sub

Sub txtSpouseBirth_KeyDown (KeyCode As Integer, Shift As Integer)

    Select Case (KeyCode)
       Case KEY_HOME, KEY_END
          If Shift = 4 Then
             Call txtSpouseBirth_LostFocus
             If KeyCode = KEY_HOME Then txtSpouseFirstName.SetFocus
             If KeyCode = KEY_END Then cmdCancel.SetFocus
          End If
       Case KEY_DOWN, KEY_UP, KEY_RETURN, KEY_ESCAPE
          If KeyCode = KEY_RETURN Then BirthChanged = 2
          If KeyCode = KEY_ESCAPE Then
             txtSpouseBirth.Text = Clipboard.GetText()
             BirthChanged = 2
          End If
          Call txtSpouseBirth_LostFocus
          If KeyCode = KEY_DOWN Then cboSpousePlan.SetFocus
          If KeyCode = KEY_UP Then txtSpouseFirstName.SetFocus
    End Select

End Sub

Sub txtSpouseBirth_KeyUp (KeyCode As Integer, Shift As Integer)
       
    If (KeyCode > 47 And KeyCode < 58) Or (KeyCode > 95 And KeyCode < 106) Then
       If Mid$(txtSpouseBirth.Text, 10, 1) <> "_" And txtSpouseBirth.Text <> Clipboard.GetText() Then
          BirthChanged = 2
          Call txtSpouseBirth_LostFocus
       End If
    End If

End Sub

Sub txtSpouseBirth_LostFocus ()

    If BirthChanged = 0 Or txtSpouseBirth.Text = "__/__/____" Then GoTo End_BirthProc
    tempbirth$ = txtSpouseBirth.Text
    Call Fetch_Life_Birth(RetCode, tempbirth$)
    If RetCode = False Then
       txtSpouseBirth.Text = "__/__/____"
       MsgText = "Invalid birthdate"
       MsgBox MsgText, , "LifeSavings"
       txtSpouseBirth.SetFocus
       Exit Sub
    End If
    If AgeChanged = 3 Then GoTo End_BirthProc
    If Left$(txtSpouseBirth.Text, 2) <> "__" Then
       mm$ = Left$(txtSpouseBirth.Text, 2)
       dd$ = Mid$(txtSpouseBirth.Text, 4, 2)
       yy$ = Right$(txtSpouseBirth.Text, 4)
       If Left$(txtSpouseAge.Text, 2) <> "  " Then
          Xyy$ = Str$(Year(Now) - Val(txtSpouseAge.Text))
          If Val(mm$) > Month(Now) Or (Val(mm$) = Month(Now) And Val(dd$) > Day(Now)) Then
             Xyy$ = Str$(Val(Xyy$) - 1)
          End If
       Else
          Xyy$ = Str$(Year(Now))
       End If
       Xdd$ = Str$(Day(Now))
       Xmm$ = Str$(Month(Now))

       If yy$ = "  " Then yy$ = Right$(Xyy$, 4)
       If dd$ = "  " Then dd$ = Xdd$
       If mm$ = "  " Then mm$ = Xmm$
       ent_date& = DateSerial(Val(yy$), Val(mm$), Val(dd$))
       curr_dt& = Fix(Now)
       Call Year_Calc(ent_date&, curr_dt&, cage%, temp_dt&)
       MsgText = ""
       If cage% > 80 Or cage% < 15 Then
          MsgText = "Birthdate must be such that issue age is between 15 and 80 for a LifeSavings spouse"
       End If
       If MsgText <> "" Then
          MsgBox MsgText, , "LifeSavings"
          txtSpouseBirth.Text = "__/__/____"
          txtSpouseBirth.SetFocus
          Exit Sub
       End If
    End If

    If txtSpouseAge.Text <> Mid$(Str$(cage%), 2, 2) Then
       txtSpouseAge.Text = Mid$(Str$(cage%), 2, 2)
    End If

End_BirthProc:
    
    If BirthChanged = 2 Then txtSpouseSex.SetFocus
    txtSpouseBirth.ForeColor = &H80000008
    txtSpouseBirth.BackColor = &H80000005

End Sub

Sub txtSpouseCurrentInt_GotFocus ()
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtSpouseCurrentInt.Text)
    txtSpouseCurrentInt.Text = ""
    txtSpouseCurrentInt.Text = Clipboard.GetText()
    txtSpouseCurrentInt.ForeColor = &H80000005
    txtSpouseCurrentInt.BackColor = &H80000008

End Sub

Sub txtSpouseCurrentInt_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then txtSpouseFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdCancel.SetFocus
    If KeyCode = KEY_RETURN Then txtSpouseState.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtSpouseCurrentInt.Text = Clipboard.GetText()
       txtSpouseState.SetFocus
    End If

End Sub

Sub txtSpouseCurrentInt_KeyPress (KeyAscii As Integer)

    If (KeyAscii < 48 Or KeyAscii > 57) And KeyAscii <> 46 Then KeyAscii = 0

End Sub

Sub txtSpouseCurrentInt_LostFocus ()
    
    Dim TempInt As Single

    txtSpouseCurrentInt.ForeColor = &H80000008
    txtSpouseCurrentInt.BackColor = &H80000005
    If Val(txtSpouseCurrentInt.Text) > 0 And (Val(txtSpouseCurrentInt.Text) < 3# Or Val(txtSpouseCurrentInt.Text) > 12#) Then
       MsgText = "Current Interest Rate must be between 3.00% and the Current Interest Rate. Do not use an Interest Rate that is greater than the Current Rate."
       MsgBox MsgText, , "LifeSavings"
       txtSpouseCurrentInt.Text = ""
       txtSpouseCurrentInt.SetFocus
       Exit Sub
    End If
    TempInt = Val(txtSpouseCurrentInt.Text)
    If TempInt > 0 Then txtSpouseCurrentInt.Text = Format$(TempInt, "00.000")

End Sub

Sub txtSpouseFace_GotFocus ()
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtSpouseFace.Text)
    txtSpouseFace.Text = "0000000"
    txtSpouseFace.Text = Clipboard.GetText()
    txtSpouseFace.ForeColor = &H80000005
    txtSpouseFace.BackColor = &H80000008

End Sub

Sub txtSpouseFace_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then txtSpouseFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdCancel.SetFocus
    If KeyCode = KEY_RETURN Then txtSpouseCurrentInt.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtSpouseFace.Text = Clipboard.GetText()
       txtSpouseCurrentInt.SetFocus
    End If

End Sub

Sub txtSpouseFace_KeyPress (KeyAscii As Integer)

    If KeyAscii < 48 Or KeyAscii > 57 Then KeyAscii = 0

End Sub

Sub txtSpouseFace_LostFocus ()

    txtSpouseFace.ForeColor = &H80000008
    txtSpouseFace.BackColor = &H80000005
    If Val(txtSpouseFace.Text) > 0 Then
       MsgText = ""
       If defFaceLimit = 0 Then
          If Val(txtSpouseFace.Text) < 15000 Or Val(txtSpouseFace.Text) > 9999999 Then
             MsgText = "Face Amount must be between $15,000 and $9,999,999, inclusive"
          End If
       Else
          If Val(txtSpouseFace.Text) < 1 Or Val(txtSpouseFace.Text) > 9999999 Then
             MsgText = "Face Amount must be between $1 and $9,999,999, inclusive"
          End If
       End If
       If MsgText <> "" Then
          MsgBox MsgText, , "LifeSavings"
          txtSpouseFace.Text = "_______"
          txtSpouseFace.SetFocus
          Exit Sub
       End If
    End If

End Sub

Sub txtSpouseFirstName_Change ()
 
    SpouseChanged = True

End Sub

Sub txtSpouseFirstName_GotFocus ()
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtSpouseFirstName.Text)
    txtSpouseFirstName.Text = ""
    txtSpouseFirstName.Text = Clipboard.GetText()
    txtSpouseFirstName.ForeColor = &H80000005
    txtSpouseFirstName.BackColor = &H80000008

End Sub

Sub txtSpouseFirstName_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_END And Shift = 4 Then cmdCancel.SetFocus
    If KeyCode = KEY_DOWN Then txtSpouseAge.SetFocus
    If KeyCode = KEY_ESCAPE Then txtSpouseFirstName.Text = Clipboard.GetText()
    If KeyCode = KEY_RETURN Or KeyCode = KEY_ESCAPE Then
       onechar$ = Left$(txtSpouseFirstName, 1)
       If onechar$ = "" Or onechar$ = " " Then
          txtSpouseFirstName.Text = "Spouse"
          txtSpouseMidInit.Text = ""
          txtSpouseLastName.Text = ""
          txtSpouseAge.SetFocus
       Else
          onechar$ = UCase(onechar$)
          txtSpouseFirstName.Text = onechar$ & Mid$(txtSpouseFirstName.Text, 2)
          txtSpouseMidInit.SetFocus
       End If
    End If

End Sub

Sub txtSpouseFirstName_LostFocus ()

    txtSpouseFirstName.ForeColor = &H80000008
    txtSpouseFirstName.BackColor = &H80000005

End Sub

Sub txtSpouseFlatRate_GotFocus ()
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtSpouseFlatRate.Text)
    txtSpouseFlatRate.Text = ""
    txtSpouseFlatRate.Text = Clipboard.GetText()
    txtSpouseFlatRate.ForeColor = &H80000005
    txtSpouseFlatRate.BackColor = &H80000008

End Sub

Sub txtSpouseFlatRate_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then txtSpouseFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdCancel.SetFocus
    If KeyCode = KEY_RETURN Then txtSpouseRiderYearEnd.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtSpouseFlatRate.Text = Clipboard.GetText()
       txtSpouseRiderYearEnd.SetFocus
    End If

End Sub

Sub txtSpouseFlatRate_KeyPress (KeyAscii As Integer)

    If (KeyAscii < 48 Or KeyAscii > 57) And KeyAscii <> 46 Then KeyAscii = 0

End Sub

Sub txtSpouseFlatRate_LostFocus ()
    
    Dim TempFlat As Single

    txtSpouseFlatRate.ForeColor = &H80000008
    txtSpouseFlatRate.BackColor = &H80000005
    If Val(txtSpouseFlatRate.Text) > 0 And (Val(txtSpouseFlatRate.Text) < 1# Or Val(txtSpouseFlatRate.Text) > 25#) Then
       MsgText = "Flat Extra Premium Amount must be between $1.00 and $25.00, inclusive"
       MsgBox MsgText, , "LifeSavings"
       txtSpouseFlatRate.Text = ""
       txtSpouseFlatRate.SetFocus
       Exit Sub
    End If
    TempFlat = Val(txtSpouseFlatRate.Text)
    If TempFlat > 0 Then txtSpouseFlatRate.Text = "     " & Format$(TempFlat, "#0.00")

End Sub

Sub txtSpouseLastName_Change ()
 
    SpouseChanged = True

End Sub

Sub txtSpouseLastName_GotFocus ()

    Clipboard.Clear
    Clipboard.SetText Trim$(txtSpouseLastName.Text)
    txtSpouseLastName.Text = ""
    txtSpouseLastName.Text = Clipboard.GetText()
    txtSpouseLastName.ForeColor = &H80000005
    txtSpouseLastName.BackColor = &H80000008

End Sub

Sub txtSpouseLastName_KeyDown (KeyCode As Integer, Shift As Integer)
     
    If KeyCode = KEY_HOME And Shift = 4 Then txtSpouseFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdCancel.SetFocus
    If KeyCode = KEY_DOWN Then txtSpouseAge.SetFocus
    If KeyCode = KEY_RETURN Then txtSpouseAge.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtSpouseLastName.Text = Clipboard.GetText()
       txtSpouseAge.SetFocus
    End If

End Sub

Sub txtSpouseLastName_LostFocus ()
    
    txtSpouseLastName.ForeColor = &H80000008
    txtSpouseLastName.BackColor = &H80000005
    onechar$ = Left$(txtSpouseLastName, 1)
    onechar$ = UCase(onechar$)
    txtSpouseLastName.Text = onechar$ & Mid$(txtSpouseLastName.Text, 2)

End Sub

Sub txtSpouseMidInit_GotFocus ()
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtSpouseMidInit.Text)
    txtSpouseMidInit.Text = ""
    txtSpouseMidInit.Text = Clipboard.GetText()
    txtSpouseMidInit.ForeColor = &H80000005
    txtSpouseMidInit.BackColor = &H80000008

End Sub

Sub txtSpouseMidInit_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then txtSpouseFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdCancel.SetFocus
    If KeyCode = KEY_DOWN Then txtSpouseAge.SetFocus
    If KeyCode = KEY_RETURN Then txtSpouseLastName.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtSpouseMidInit.Text = Clipboard.GetText()
       txtSpouseLastName.SetFocus
    End If

End Sub

Sub txtSpouseMidInit_LostFocus ()
     
    txtSpouseMidInit.ForeColor = &H80000008
    txtSpouseMidInit.BackColor = &H80000005
    txtSpouseMidInit.Text = UCase$(txtSpouseMidInit.Text)

End Sub

Sub txtSpouseRiderYearEnd_GotFocus ()
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtSpouseRiderYearEnd.Text)
    txtSpouseRiderYearEnd.Text = "__"
    If Val(Clipboard.GetText()) <> 0 Then
       txtSpouseRiderYearEnd.Text = Clipboard.GetText()
    End If
    txtSpouseRiderYearEnd.ForeColor = &H80000005
    txtSpouseRiderYearEnd.BackColor = &H80000008

End Sub

Sub txtSpouseRiderYearEnd_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then txtSpouseFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdOK.SetFocus
    If KeyCode = KEY_RETURN Then cboSpouseRiderYrAgeEnd.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtSpouseRiderYearEnd.Text = Clipboard.GetText()
       cboSpouseRiderYrAgeEnd.SetFocus
    End If

End Sub

Sub txtSpouseRiderYearEnd_LostFocus ()
    
    txtSpouseRiderYearEnd.ForeColor = &H80000008
    txtSpouseRiderYearEnd.BackColor = &H80000005

End Sub

Sub txtSpouseSex_Change ()
 
    SpouseChanged = True

End Sub

Sub txtSpouseSex_GotFocus ()
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtSpouseSex.Text)
    txtSpouseSex.Text = ""
    txtSpouseSex.Text = Clipboard.GetText()
    txtSpouseSex.ForeColor = &H80000005
    txtSpouseSex.BackColor = &H80000008

End Sub

Sub txtSpouseSex_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then txtSpouseFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdCancel.SetFocus
    If KeyCode = KEY_UP Then txtSpouseFirstName.SetFocus
    If KeyCode = KEY_DOWN Then txtSpouseFace.SetFocus
    If KeyCode = KEY_RETURN Then cboSpousePlan.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtSpouseSex.Text = Clipboard.GetText()
       cboSpousePlan.SetFocus
    End If

End Sub

Sub txtSpouseSex_KeyPress (KeyAscii As Integer)
     
    Select Case (KeyAscii)
       Case 32, 70, 77, 102, 109
          If txtSpouseSex.Text = Clipboard.GetText() Then
             txtSpouseSex.Text = ""
             cboSpousePlan.SetFocus
          End If
       Case Else
          KeyAscii = 0
    End Select

End Sub

Sub txtSpouseSex_LostFocus ()
    
    txtSpouseSex.ForeColor = &H80000008
    txtSpouseSex.BackColor = &H80000005
    txtSpouseSex.Text = UCase$(txtSpouseSex.Text)

End Sub

Sub txtSpouseState_GotFocus ()
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtSpouseState.Text)
    txtSpouseState.Text = ""
    txtSpouseState.Text = Clipboard.GetText()
    txtSpouseState.ForeColor = &H80000005
    txtSpouseState.BackColor = &H80000008

End Sub

Sub txtSpouseState_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then txtSpouseFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdCancel.SetFocus
    If KeyCode = KEY_UP Then txtSpouseFirstName.SetFocus
    If KeyCode = KEY_DOWN Then cboSpouseWPD.SetFocus
    If KeyCode = KEY_RETURN Then cboSpouseWPD.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtSpouseState.Text = Clipboard.GetText()
       cboSpouseWPD.SetFocus
    End If

End Sub

Sub txtSpouseState_KeyPress (KeyAscii As Integer)
    
    If KeyAscii = 32 Or (KeyAscii > 64 And KeyAscii < 91) Or (KeyAscii > 96 And KeyAscii < 123) Then
       If txtSpouseState.Text = Clipboard.GetText() Then
          txtSpouseState.Text = ""
       End If
       If Len(txtSpouseState.Text) > 0 Then
          TempChar$ = Left$(txtSpouseState.Text, 1) & Chr$(KeyAscii)
          TempChar$ = UCase(TempChar$)
          RetCode = State_Verify(TempChar$)
          If RetCode <> -1 Then
             Select Case RetCode
                Case 20, 29, 31, 37
                   txtSpouseState.Text = TempChar$
                   MsgText = "LifeSavings is not available in the state of " & TempChar$
                   MsgBox MsgText, , "LifeSavings"
                Case Else
                   MsgText = ""
             End Select
             If MsgText = "" Then
                cboSpouseWPD.SetFocus
             Else
                KeyAscii = 0
                txtSpouseState.Text = ""
                txtSpouseState.SetFocus
             End If
          Else
             KeyAscii = 0
             txtSpouseState.Text = ""
             txtSpouseState.SetFocus
          End If
       End If
    End If

End Sub

Sub txtSpouseState_LostFocus ()

    Dim TempChar As String * 2

    txtSpouseState.ForeColor = &H80000008
    txtSpouseState.BackColor = &H80000005
    txtSpouseState.Text = UCase(txtSpouseState.Text)
    TempChar = txtSpouseState.Text
    If Len(TempChar) > 0 Then
       RetCode = State_Verify(TempChar)
       If RetCode = -1 Then
          txtSpouseState.Text = ""
          txtSpouseState.SetFocus
       End If
    End If
    
    If TempChar = "MI" Then
       lblSpouseState.Caption = "Spouse Tax ID:"
       txtSpouseTaxID.Enabled = True
       txtSpouseTaxID.Visible = True
       txtSpouseTaxID.SetFocus
    End If

End Sub

Sub txtSpouseTaxID_GotFocus ()
    
    txtSpouseTaxID.ForeColor = &H80000005
    txtSpouseTaxID.BackColor = &H80000008

End Sub

Sub txtSpouseTaxID_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_UP Then txtSpouseFace.SetFocus
    If KeyCode = KEY_DOWN Or KeyCode = KEY_RETURN Or KeyCode = KEY_ESCAPE Then cboSpouseWPD.SetFocus

End Sub

Sub txtSpouseTaxID_LostFocus ()
       
    If Val(txtSpouseTaxID.Text) = 0 Then txtSpouseTaxID.Text = "999-99-9999"
    lblSpouseState.Caption = "State of Residence:"
    txtSpouseTaxID.ForeColor = &H80000008
    txtSpouseTaxID.BackColor = &H80000005
    txtSpouseTaxID.Enabled = False
    txtSpouseTaxID.Visible = False
    cboSpouseWPD.SetFocus

End Sub

