VERSION 2.00
Begin Form frmLSSPOUSE 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Life Savings II - Spouse As Principal Insured Parameters"
   ClientHeight    =   6915
   ClientLeft      =   60
   ClientTop       =   420
   ClientWidth     =   9585
   ControlBox      =   0   'False
   Height          =   7395
   Left            =   0
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   6915
   ScaleWidth      =   9585
   Top             =   0
   Width           =   9705
   Begin TextBox txtSpouseAge 
      Height          =   285
      Left            =   1440
      MaxLength       =   2
      TabIndex        =   3
      Top             =   480
      Width           =   375
   End
   Begin TextBox txtSpouseCurrentInt 
      Height          =   285
      Left            =   2160
      MaxLength       =   6
      TabIndex        =   9
      Top             =   1200
      Width           =   975
   End
   Begin TextBox txtSpouseState 
      Height          =   285
      Left            =   8880
      MaxLength       =   2
      TabIndex        =   6
      Top             =   480
      Width           =   495
   End
   Begin Frame fraSpouseExtras 
      Caption         =   "Illustrate Using:"
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   1455
      Index           =   1
      Left            =   120
      TabIndex        =   42
      Top             =   3000
      Width           =   9255
      Begin SSCommand cmdFuture 
         BevelWidth      =   4
         Caption         =   "&FUTURE Changes"
         Height          =   495
         Left            =   7200
         TabIndex        =   23
         Top             =   840
         Width           =   1815
      End
      Begin SSCommand cmdIllustrate 
         BevelWidth      =   4
         Caption         =   "&ILLUSTRATE"
         Height          =   495
         Left            =   2400
         TabIndex        =   21
         Top             =   840
         Width           =   1935
      End
      Begin SSCommand cmdPremium 
         BevelWidth      =   4
         Caption         =   "&PREMIUM CALC"
         Height          =   495
         Left            =   4800
         TabIndex        =   22
         Top             =   840
         Width           =   1935
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
         TabIndex        =   19
         TabStop         =   0   'False
         Top             =   720
         Width           =   2055
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
         TabIndex        =   20
         TabStop         =   0   'False
         Top             =   1080
         Width           =   2055
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
         TabIndex        =   18
         Top             =   360
         Value           =   -1  'True
         Width           =   2055
      End
      Begin Label lblIll3 
         Alignment       =   2  'Center
         Caption         =   "Display and Make Policy Changes for Future Years Before Illustrating"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   495
         Left            =   7200
         TabIndex        =   45
         Top             =   240
         Width           =   1815
      End
      Begin Label lblIll2 
         Alignment       =   2  'Center
         Caption         =   "Display/Change Planned Premium, Mode, Lump Sum Payments or Target Cash Values Before Illustrating"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   495
         Left            =   4560
         TabIndex        =   44
         Top             =   240
         Width           =   2415
      End
      Begin Label lblIll1 
         Alignment       =   2  'Center
         Caption         =   """Quick"" Illustration Using Current Options (above) and Annual Premium (selected at left)"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   495
         Left            =   2280
         TabIndex        =   43
         Top             =   240
         Width           =   2175
      End
   End
   Begin TextBox txtSpouseLastName 
      Height          =   285
      Left            =   5280
      MaxLength       =   20
      TabIndex        =   2
      Top             =   120
      Width           =   4095
   End
   Begin TextBox txtSpouseMidInit 
      Height          =   285
      Left            =   4920
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
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   1335
      Index           =   0
      Left            =   120
      TabIndex        =   36
      Top             =   1560
      Width           =   9255
      Begin ComboBox cboSpouseRiderYrAgeEnd 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Left            =   8040
         Style           =   2  'Dropdown List
         TabIndex        =   17
         Top             =   960
         Width           =   855
      End
      Begin MaskEdBox txtSpouseRiderYearEnd 
         Height          =   285
         Left            =   7680
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   "_"
         TabIndex        =   16
         Top             =   960
         Width           =   375
      End
      Begin MaskEdBox txtSpouseTermRider 
         Height          =   285
         Left            =   6240
         Mask            =   "#######"
         MaxLength       =   7
         PromptChar      =   "_"
         TabIndex        =   15
         Top             =   960
         Width           =   975
      End
      Begin ComboBox cboSpouseWPD 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   315
         Left            =   1800
         Style           =   2  'Dropdown List
         TabIndex        =   12
         Top             =   960
         Width           =   975
      End
      Begin ComboBox cboSpouseCOLA 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   300
         Left            =   4800
         Style           =   2  'Dropdown List
         TabIndex        =   14
         Top             =   960
         Width           =   975
      End
      Begin ComboBox cboSpouseGPO 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   315
         Left            =   3240
         Style           =   2  'Dropdown List
         TabIndex        =   13
         Top             =   960
         Width           =   1095
      End
      Begin ComboBox cboSpouseSubStd 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   315
         Left            =   360
         Style           =   2  'Dropdown List
         TabIndex        =   11
         Top             =   960
         Width           =   975
      End
      Begin Label lblSpouseRider 
         Alignment       =   2  'Center
         Caption         =   "Years or To Age"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   2
         Left            =   7680
         TabIndex        =   51
         Top             =   600
         Width           =   1215
      End
      Begin Label lblSpouseRider 
         Alignment       =   2  'Center
         Caption         =   "Death Benefit Amount"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Index           =   1
         Left            =   6240
         TabIndex        =   50
         Top             =   600
         Width           =   975
      End
      Begin Label lblWPD 
         Alignment       =   2  'Center
         Caption         =   "Disability Waiver of Cost of Insurance Rider"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   495
         Left            =   1680
         TabIndex        =   39
         Top             =   360
         Width           =   1215
      End
      Begin Label lblPrinCOLA 
         Alignment       =   2  'Center
         Caption         =   "Cost of Living Increase Benefit Rider"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   495
         Left            =   4800
         TabIndex        =   40
         Top             =   360
         Width           =   975
      End
      Begin Label lblGPO 
         Alignment       =   2  'Center
         Caption         =   "Guaranteed Added Death Benefit Rider"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   495
         Left            =   3240
         TabIndex        =   38
         Top             =   360
         Width           =   1095
      End
      Begin Label lblSpouseRider 
         Alignment       =   2  'Center
         Caption         =   "- - - - - - - - Term Insurance Rider - - - - - - - -"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   0
         Left            =   6240
         TabIndex        =   41
         Top             =   360
         Width           =   2655
      End
      Begin Label lblSpouseSubStd 
         Alignment       =   2  'Center
         Caption         =   "Table Rating (Substandard)"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Left            =   360
         TabIndex        =   37
         Top             =   360
         Width           =   975
      End
   End
   Begin ComboBox cboSpouseOption 
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   8.25
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   315
      Left            =   6600
      Style           =   2  'Dropdown List
      TabIndex        =   10
      Top             =   1200
      Width           =   2775
   End
   Begin MaskEdBox txtSpouseFace 
      Height          =   285
      Left            =   8400
      Mask            =   "#######"
      MaxLength       =   7
      PromptChar      =   "_"
      TabIndex        =   8
      Top             =   840
      Width           =   975
   End
   Begin TextBox txtSpouseSex 
      Height          =   285
      Left            =   6600
      MaxLength       =   1
      TabIndex        =   5
      Top             =   480
      Width           =   375
   End
   Begin ComboBox cboSpousePlan 
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   8.25
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   300
      Left            =   1440
      Style           =   2  'Dropdown List
      TabIndex        =   7
      Top             =   840
      Width           =   1695
   End
   Begin MaskEdBox txtSpouseBirth 
      Height          =   285
      Left            =   3480
      Mask            =   "##/##/####"
      MaxLength       =   10
      PromptChar      =   "_"
      TabIndex        =   4
      Top             =   480
      Width           =   1335
   End
   Begin TextBox txtSpouseFirstName 
      Height          =   285
      Left            =   1440
      MaxLength       =   20
      TabIndex        =   0
      Top             =   120
      Width           =   3375
   End
   Begin Frame fraSpouseCommands 
      Height          =   2175
      Left            =   1320
      TabIndex        =   46
      Top             =   4560
      Width           =   6855
      Begin SSCommand cmdCancel 
         BevelWidth      =   4
         Caption         =   "E&XIT"
         Font3D          =   3  'Inset w/light shading
         ForeColor       =   &H00000000&
         Height          =   495
         Left            =   240
         TabIndex        =   26
         Top             =   840
         Width           =   975
      End
      Begin SSCommand cmdSave 
         BevelWidth      =   4
         Caption         =   "&SAVE"
         Font3D          =   3  'Inset w/light shading
         ForeColor       =   &H00000000&
         Height          =   495
         Left            =   240
         TabIndex        =   25
         Top             =   1440
         Width           =   975
      End
      Begin SSCommand cmdOK 
         BevelWidth      =   4
         Caption         =   "&OK"
         Font3D          =   3  'Inset w/light shading
         ForeColor       =   &H00000000&
         Height          =   495
         Left            =   240
         TabIndex        =   24
         Top             =   240
         Width           =   975
      End
      Begin Label lblSpouseCommand 
         Caption         =   "Return to Principal Insured Parameter Display; ignore all changes "
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   2
         Left            =   1560
         TabIndex        =   49
         Top             =   960
         Width           =   5175
      End
      Begin Label lblSpouseCommand 
         Caption         =   "Save Data as separate New Client and return to Principal  Insured"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   1
         Left            =   1560
         TabIndex        =   48
         Top             =   1560
         Width           =   5175
      End
      Begin Label lblSpouseCommand 
         Caption         =   "Retain Data and Return to Principal Insured Parameter Display"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   0
         Left            =   1560
         TabIndex        =   47
         Top             =   360
         Width           =   5175
      End
   End
   Begin Label lblSpouseInt 
      Caption         =   "Current Interest Rate:"
      Height          =   255
      Left            =   120
      TabIndex        =   34
      Top             =   1200
      Width           =   1935
   End
   Begin Label lblSpouseState 
      Caption         =   "State of Residence:"
      Height          =   255
      Left            =   7080
      TabIndex        =   31
      Top             =   480
      Width           =   1815
   End
   Begin Label lblSpouseOption 
      Caption         =   "Option Code:"
      Height          =   255
      Left            =   5280
      TabIndex        =   35
      Top             =   1200
      Width           =   1215
   End
   Begin Label lblSpouseFace 
      Caption         =   "Initial Specified Death Benefit:"
      Height          =   255
      Left            =   5280
      TabIndex        =   33
      Top             =   840
      Width           =   2775
   End
   Begin Label lblSpouseSex 
      Caption         =   "Gender:"
      Height          =   255
      Left            =   5280
      TabIndex        =   30
      Top             =   480
      Width           =   855
   End
   Begin Label lblSpousePlan 
      Caption         =   "Plan Type:"
      Height          =   255
      Left            =   120
      TabIndex        =   32
      Top             =   840
      Width           =   975
   End
   Begin Label lblSpouseBirth 
      Caption         =   "or Birthdate:"
      Height          =   255
      Left            =   2040
      TabIndex        =   29
      Top             =   480
      Width           =   1215
   End
   Begin Label lblSpouseAge 
      Caption         =   "Spouse Age:"
      Height          =   255
      Left            =   120
      TabIndex        =   28
      Top             =   480
      Width           =   1215
   End
   Begin Label lblSpouseName 
      Caption         =   "Spouse Name:"
      Height          =   255
      Left            =   120
      TabIndex        =   27
      Top             =   120
      Width           =   1455
   End
End

Sub cboSpouseCOLA_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then txtSpouseTermRider.SetFocus

End Sub

Sub cboSpouseCOLA_LostFocus ()
    
    If Val(txtSpouseAge.Text) > 60 Then
       If cboSpouseCOLA.ListIndex <> 0 Then
          msgline$ = "Cost of Living Rider not allowed; Spouse must be less than age 61"
          MsgBox msgline$
          Exit Sub
       End If
    End If

    If cboSpouseSubStd.ListIndex > 0 Then
       If cboSpouseCOLA.ListIndex <> 0 Then
          msgline$ = "Cost of Living Rider not allowed if Spouse has substandard Table Rating"
          MsgBox msgline$
          cboSpouseWPD.ListIndex = 0
          Exit Sub
       End If
    End If

End Sub

Sub cboSpouseGPO_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then cboSpouseCOLA.SetFocus

End Sub

Sub cboSpouseGPO_LostFocus ()
    
    If Val(txtSpouseAge.Text) > 39 Then
       If cboSpouseGPO.ListIndex <> 0 Then
          msgline$ = "Guaranteed Added Death Benefit Rider not allowed; Spouse must be less than age 40"
          MsgBox msgline$
          cboSpouseGPO.ListIndex = 0
          Exit Sub
       End If
    End If
    
    If cboSpouseSubStd.ListIndex > 0 Then
       If cboSpouseGPO.ListIndex <> 0 Then
          msgline$ = "Guaranteed Added Death Benefit Rider not allowed if Spouse has substandard Table Rating"
          MsgBox msgline$
          cboSpouseGPO.ListIndex = 0
          Exit Sub
       End If
    End If
    
End Sub

Sub cboSpouseOption_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then cboSpouseSubStd.SetFocus

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

    If txtSpouseTermRider.Text = "_______" Then Exit Sub
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
             msgline$ = "Age for Rider must be greater than Spouse Issue Age"
             MsgBox msgline$
             txtSpouseRiderYearEnd.SetFocus
             Exit Sub
          End If
       Case Else
          If Val(txtSpouseRiderYearEnd.Text) > 95 - TempAge Then
             msgline$ = "Years for Rider cannot be greater than Spouse Age 95"
             MsgBox msgline$
             txtSpouseRiderYearEnd.SetFocus
             Exit Sub
          End If
    End Select

End Sub

Sub cboSpouseSubStd_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then cboSpouseWPD.SetFocus

End Sub

Sub cboSpouseSubStd_LostFocus ()

    If cboSpouseSubStd.ListIndex > 0 Then
       cboSpouseWPD.ListIndex = 0    'not allowed if substd
       cboSpouseGPO.ListIndex = 0
       cboSpouseCOLA.ListIndex = 0
    End If

End Sub

Sub cboSpouseWPD_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then cboSpouseGPO.SetFocus

End Sub

Sub cboSpouseWPD_LostFocus ()
    
    If Val(txtSpouseAge.Text) > 55 Then
       If cboSpouseWPD.ListIndex <> 0 Then
          msgline$ = "Disability Waiver of Cost Rider not allowed; Spouse must be less than age 56"
          MsgBox msgline$
          cboSpouseWPD.ListIndex = 0
          Exit Sub
       End If
    End If
    
    If Val(txtSpouseAge.Text) < 15 Then
       If cboSpouseWPD.ListIndex <> 0 Then
          msgline$ = "Disability Waiver of Cost Rider not allowed; Spouse must be greater than age 14"
          MsgBox msgline$
          cboSpouseWPD.ListIndex = 0
          Exit Sub
       End If
    End If
    
    If cboSpouseSubStd.ListIndex > 0 Then
       If cboSpouseWPD.ListIndex <> 0 Then
          msgline$ = "Disability Waiver of Cost Rider not allowed if Spouse has substandard Table Rating"
          MsgBox msgline$
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

    LifeIllusType = 1
   
    s = SpouseClient
    Call Set_Spouse_Illus_Parameters

    i = ReadyToIllustrate()
    If i = False Then
       msgtext$ = "Cannot Illustrate until valid Age, Sex, Death Benefit and Interest Rate are input"
       MsgBox msgtext$
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
    If LSClient(0).CurrRate > 0 Then txtSpouseCurrentInt.Text = Format$(LSClient(0).CurrRate, "00.000")
    If LSClient(0).State <> "" Then txtSpouseState.Text = LSClient(0).State

    cboSpousePlan.AddItem "NonSmoker"
    cboSpousePlan.AddItem "Smoker"
    cboSpousePlan.ListIndex = 0
    cboSpouseOption.AddItem "1 - Level Death Benefit"
    cboSpouseOption.AddItem "2 - Increasing Death Benefit"
    cboSpouseOption.ListIndex = 0
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
    cboSpouseCOLA.AddItem "None"
    cboSpouseCOLA.AddItem "1%"
    cboSpouseCOLA.AddItem "2%"
    cboSpouseCOLA.AddItem "3%"
    cboSpouseCOLA.AddItem "4%"
    cboSpouseCOLA.AddItem "5%"
    cboSpouseCOLA.AddItem "6%"
    cboSpouseCOLA.AddItem "7%"
    cboSpouseCOLA.AddItem "8%"
    cboSpouseCOLA.AddItem "9%"
    cboSpouseCOLA.AddItem "10%"
    cboSpouseCOLA.ListIndex = 0
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
 
    clipboard.Clear
    clipboard.SetText Trim$(txtSpouseAge.Text)
    txtSpouseAge.Text = ""
    txtSpouseAge.Text = clipboard.GetText()
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
             txtSpouseAge.Text = clipboard.GetText()
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
       If txtSpouseAge.Text = clipboard.GetText() Then
          txtSpouseAge.Text = ""
       End If
    End If

End Sub

Sub txtSpouseAge_KeyUp (KeyCode As Integer, Shift As Integer)

    If KeyCode > 47 And KeyCode < 58 Then
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
    msgtext$ = ""
    If TempAge > 70 Then
       msgtext$ = "Issue age must be less than 71 for a Life Savings II Spouse"
    End If
    If TempAge < 15 Then
       msgtext$ = "Issue age must be at least age 15 for a Life Savings II Spouse"
    End If
    If msgtext$ <> "" Then
       MsgBox msgtext$
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
             cboSpouseCOLA.ListIndex = 0
             If TempAge > 65 Then
                txtSpouseTermRider.Text = ""
                txtSpouseRiderYearEnd.Text = ""
                cboSpouseRiderYrAgeEnd.ListIndex = 0
             End If
          End If
       End If
    End If

End_AgeProc:
    
    If AgeChanged >= 2 Then txtSpouseBirth.SetFocus
    If Val(txtSpouseFace.Text) > 0 Or Val(txtSpouseTermRider.Text) > 0 Then
       Select Case TempAge
          Case 61 To 70
             If Val(txtSpouseFace.Text) > 0 And Val(txtSpouseFace.Text) < 15000 Then
                txtSpouseFace.Text = "15000__"
             End If
             If Val(txtSpouseTermRider.Text) > 0 And Val(txtSpouseTermRider.Text) < 15000 Then
                txtSpouseTermRider.Text = "15000__"
             End If
          Case 51 To 60
             If Val(txtSpouseFace.Text) > 0 And Val(txtSpouseFace.Text) < 20000 Then
                txtSpouseFace.Text = "20000__"
             End If
             If Val(txtSpouseTermRider.Text) > 0 And Val(txtSpouseTermRider.Text) < 20000 Then
                txtSpouseTermRider.Text = "20000__"
             End If
          Case Else
             If Val(txtSpouseFace.Text) > 0 And Val(txtSpouseFace.Text) < 25000 Then
                txtSpouseFace.Text = "25000__"
             End If
             If Val(txtSpouseTermRider.Text) > 0 And Val(txtSpouseTermRider.Text) < 25000 Then
                txtSpouseTermRider.Text = "25000__"
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
 
    clipboard.Clear
    clipboard.SetText Trim$(txtSpouseBirth.Text)
    txtSpouseBirth.Text = "__/__/____"
    txtSpouseBirth.Text = clipboard.GetText()
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
             txtSpouseBirth.Text = clipboard.GetText()
             BirthChanged = 2
          End If
          Call txtSpouseBirth_LostFocus
          If KeyCode = KEY_DOWN Then cboSpousePlan.SetFocus
          If KeyCode = KEY_UP Then txtSpouseFirstName.SetFocus
    End Select

End Sub

Sub txtSpouseBirth_KeyUp (KeyCode As Integer, Shift As Integer)
       
    If KeyCode > 47 And KeyCode < 58 Then
       If Mid$(txtSpouseBirth.Text, 10, 1) <> "_" And txtSpouseBirth.Text <> clipboard.GetText() Then
          BirthChanged = 2
          Call txtSpouseBirth_LostFocus
       End If
    End If

End Sub

Sub txtSpouseBirth_LostFocus ()

    If BirthChanged = 0 Or AgeChanged = 3 Or txtSpouseBirth.Text = "__/__/____" Then GoTo End_BirthProc
    tempbirth$ = txtSpouseBirth.Text
    Call Verify_Life_Birth(retcode%, tempbirth$)
    If retcode% = False Then
       txtSpouseBirth.Text = "__/__/____"
       msgtext$ = "Invalid birthdate"
       MsgBox msgtext$
       txtSpouseBirth.SetFocus
       Exit Sub
    End If
    If Left$(txtSpouseBirth.Text, 2) <> "__" Then
       mm$ = Left$(txtSpouseBirth.Text, 2)
       dd$ = Mid$(txtSpouseBirth.Text, 4, 2)
       yy$ = Right$(txtSpouseBirth.Text, 4)
       If Left$(txtSpouseAge.Text, 2) <> "  " Then
          Xyy$ = Str$(Year(Now) - Val(txtSpouseAge.Text))
          If Val(mm$) > Month(Now) Or (Val(mm$) = Month(Now) And Val(dd$) > Day(Now)) Then
             Xyy$ = Str$(Val(Xyy$) - 1)
          End If
       Else Xyy$ = Str$(Year(Now))
       End If
       Xdd$ = Str$(Day(Now))
       Xmm$ = Str$(Month(Now))

       If yy$ = "  " Then yy$ = Right$(Xyy$, 4)
       If dd$ = "  " Then dd$ = Xdd$
       If mm$ = "  " Then mm$ = Xmm$
       ent_date& = DateSerial(Val(yy$), Val(mm$), Val(dd$))
       curr_dt& = Fix(Now)
       Call Year_Calc(ent_date&, curr_dt&, cage%, temp_dt&)
       msgtext$ = ""
       If cage% > 70 Then
          msgtext$ = "Birthdate must be such that Issue Age is between 20 and 75 for the Tailored Term 10 Illustration"
       End If
       If msgtext$ <> "" Then
          MsgBox msgtext$
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
    
    clipboard.Clear
    clipboard.SetText Trim$(txtSpouseCurrentInt.Text)
    txtSpouseCurrentInt.Text = ""
    txtSpouseCurrentInt.Text = clipboard.GetText()
    txtSpouseCurrentInt.ForeColor = &H80000005
    txtSpouseCurrentInt.BackColor = &H80000008

End Sub

Sub txtSpouseCurrentInt_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then txtSpouseFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdCancel.SetFocus
    If KeyCode = KEY_RETURN Then cboSpouseOption.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtSpouseCurrentInt.Text = clipboard.GetText()
       cboSpouseOption.SetFocus
    End If

End Sub

Sub txtSpouseCurrentInt_KeyPress (KeyAscii As Integer)

    If (KeyAscii < 48 Or KeyAscii > 57) And KeyAscii <> 46 Then KeyAscii = 0

End Sub

Sub txtSpouseCurrentInt_LostFocus ()
    
    Dim TempInt As Single

    txtSpouseCurrentInt.ForeColor = &H80000008
    txtSpouseCurrentInt.BackColor = &H80000005
    If Val(txtSpouseCurrentInt.Text) > 0 And (Val(txtSpouseCurrentInt.Text) < 4# Or Val(txtSpouseCurrentInt.Text) > 20#) Then
       msgline$ = "Current Interest Amount must be between 4.00% and 20.00%, inclusive"
       MsgBox msgline$
       txtSpouseCurrentInt.Text = ""
       txtSpouseCurrentInt.SetFocus
       Exit Sub
    End If
    TempInt = Val(txtSpouseCurrentInt.Text)
    If TempInt > 0 Then txtSpouseCurrentInt.Text = Format$(TempInt, "00.000")

End Sub

Sub txtSpouseFace_GotFocus ()
    
    clipboard.Clear
    clipboard.SetText Trim$(txtSpouseFace.Text)
    txtSpouseFace.Text = "0000000"
    txtSpouseFace.Text = clipboard.GetText()
    txtSpouseFace.ForeColor = &H80000005
    txtSpouseFace.BackColor = &H80000008

End Sub

Sub txtSpouseFace_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then txtSpouseFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdCancel.SetFocus
    If KeyCode = KEY_RETURN Then txtSpouseCurrentInt.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtSpouseFace.Text = clipboard.GetText()
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
       msgline$ = ""
       If Val(txtSpouseFace.Text) < 15000 Or Val(txtSpouseFace.Text) > 9999999 Then
          msgline$ = "Face Amount must be between $15,000 and $9,999,999, inclusive"
       End If
       If Val(txtSpouseFace.Text) < 20000 And Val(txtSpouseAge.Text) < 61 Then
          msgline$ = "Face Amount must be at least $20,000 for a Spouse age " & Format$(Val(txtSpouseAge.Text), "#0")
       End If
       If Val(txtSpouseFace.Text) < 25000 And Val(txtSpouseAge.Text) < 51 Then
          msgline$ = "Face Amount must be at least $25,000 for a Spouse age " & Format$(Val(txtSpouseAge.Text), "#0")
       End If
       If msgline$ <> "" Then
          MsgBox msgline$
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
    
    clipboard.Clear
    clipboard.SetText Trim$(txtSpouseFirstName.Text)
    txtSpouseFirstName.Text = ""
    txtSpouseFirstName.Text = clipboard.GetText()
    txtSpouseFirstName.ForeColor = &H80000005
    txtSpouseFirstName.BackColor = &H80000008

End Sub

Sub txtSpouseFirstName_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_END And Shift = 4 Then cmdCancel.SetFocus
    If KeyCode = KEY_DOWN Then txtSpouseAge.SetFocus
    If KeyCode = KEY_ESCAPE Then txtSpouseFirstName.Text = clipboard.GetText()
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

Sub txtSpouseLastName_Change ()
 
    SpouseChanged = True

End Sub

Sub txtSpouseLastName_GotFocus ()

    clipboard.Clear
    clipboard.SetText Trim$(txtSpouseLastName.Text)
    txtSpouseLastName.Text = ""
    txtSpouseLastName.Text = clipboard.GetText()
    txtSpouseLastName.ForeColor = &H80000005
    txtSpouseLastName.BackColor = &H80000008

End Sub

Sub txtSpouseLastName_KeyDown (KeyCode As Integer, Shift As Integer)
     
    If KeyCode = KEY_HOME And Shift = 4 Then txtSpouseFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdCancel.SetFocus
    If KeyCode = KEY_DOWN Then txtSpouseAge.SetFocus
    If KeyCode = KEY_RETURN Then txtSpouseAge.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtSpouseLastName.Text = clipboard.GetText()
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
    
    clipboard.Clear
    clipboard.SetText Trim$(txtSpouseMidInit.Text)
    txtSpouseMidInit.Text = ""
    txtSpouseMidInit.Text = clipboard.GetText()
    txtSpouseMidInit.ForeColor = &H80000005
    txtSpouseMidInit.BackColor = &H80000008

End Sub

Sub txtSpouseMidInit_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then txtSpouseFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdCancel.SetFocus
    If KeyCode = KEY_DOWN Then txtSpouseAge.SetFocus
    If KeyCode = KEY_RETURN Then txtSpouseLastName.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtSpouseMidInit.Text = clipboard.GetText()
       txtSpouseLastName.SetFocus
    End If

End Sub

Sub txtSpouseMidInit_LostFocus ()
     
    txtSpouseMidInit.ForeColor = &H80000008
    txtSpouseMidInit.BackColor = &H80000005
    txtSpouseMidInit.Text = UCase$(txtSpouseMidInit.Text)

End Sub

Sub txtSpouseRiderYearEnd_GotFocus ()
    
    clipboard.Clear
    clipboard.SetText Trim$(txtSpouseRiderYearEnd.Text)
    txtSpouseRiderYearEnd.Text = "__"
    If Val(clipboard.GetText()) <> 0 Then
       txtSpouseRiderYearEnd.Text = clipboard.GetText()
    End If
    txtSpouseRiderYearEnd.ForeColor = &H80000005
    txtSpouseRiderYearEnd.BackColor = &H80000008

End Sub

Sub txtSpouseRiderYearEnd_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then txtSpouseFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdOK.SetFocus
    If KeyCode = KEY_RETURN Then cboSpouseRiderYrAgeEnd.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtSpouseRiderYearEnd.Text = clipboard.GetText()
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
    
    clipboard.Clear
    clipboard.SetText Trim$(txtSpouseSex.Text)
    txtSpouseSex.Text = ""
    txtSpouseSex.Text = clipboard.GetText()
    txtSpouseSex.ForeColor = &H80000005
    txtSpouseSex.BackColor = &H80000008

End Sub

Sub txtSpouseSex_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then txtSpouseFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdCancel.SetFocus
    If KeyCode = KEY_UP Then txtSpouseFirstName.SetFocus
    If KeyCode = KEY_DOWN Then cboSpousePlan.SetFocus
    If KeyCode = KEY_RETURN Then txtSpouseState.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtSpouseSex.Text = clipboard.GetText()
       txtSpouseState.SetFocus
    End If

End Sub

Sub txtSpouseSex_KeyPress (KeyAscii As Integer)
     
    Select Case (KeyAscii)
       Case 32, 70, 77, 102, 109
          If txtSpouseSex.Text = clipboard.GetText() Then
             txtSpouseSex.Text = ""
             txtSpouseState.SetFocus
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
    
    clipboard.Clear
    clipboard.SetText Trim$(txtSpouseState.Text)
    txtSpouseState.Text = ""
    txtSpouseState.Text = clipboard.GetText()
    txtSpouseState.ForeColor = &H80000005
    txtSpouseState.BackColor = &H80000008

End Sub

Sub txtSpouseState_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then txtSpouseFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdCancel.SetFocus
    If KeyCode = KEY_UP Then txtSpouseFirstName.SetFocus
    If KeyCode = KEY_DOWN Then cboSpousePlan.SetFocus
    If KeyCode = KEY_RETURN Then cboSpousePlan.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtSpouseState.Text = clipboard.GetText()
       cboSpousePlan.SetFocus
    End If

End Sub

Sub txtSpouseState_KeyPress (KeyAscii As Integer)
    
    If KeyAscii = 32 Or (KeyAscii > 64 And KeyAscii < 91) Or (KeyAscii > 96 And KeyAscii < 123) Then
       If txtSpouseState.Text = clipboard.GetText() Then
          txtSpouseState.Text = ""
       End If
       If Len(txtSpouseState.Text) > 0 Then
          tempchar$ = Left$(txtSpouseState.Text, 1) & Chr$(KeyAscii)
          tempchar$ = UCase(tempchar$)
          RetCd% = State_Verify(tempchar$)
          If RetCd% <> -1 Then
             Select Case RetCd%
                Case 20, 29, 31, 37
                   txtSpouseState.Text = tempchar$
                   msgtext$ = "Life Savings 2 is not available in the state of " & tempchar$
                   MsgBox msgtext$
                Case Else
                   msgtext$ = ""
             End Select
             If msgtext$ = "" Then
                cboSpousePlan.SetFocus
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

    txtSpouseState.ForeColor = &H80000008
    txtSpouseState.BackColor = &H80000005
    txtSpouseState.Text = UCase(txtSpouseState.Text)
    tempchar$ = txtSpouseState.Text
    If Len(tempchar$) > 0 Then
       RetCd% = State_Verify(tempchar$)
       If RetCd% = -1 Then
          txtSpouseState.Text = ""
          txtSpouseState.SetFocus
       End If
    End If

End Sub

Sub txtSpouseTermRider_GotFocus ()
    
    clipboard.Clear
    clipboard.SetText Trim$(txtSpouseTermRider.Text)
    txtSpouseTermRider.Text = "_______"
    txtSpouseTermRider.Text = clipboard.GetText()
    txtSpouseTermRider.ForeColor = &H80000005
    txtSpouseTermRider.BackColor = &H80000008

End Sub

Sub txtSpouseTermRider_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then txtSpouseFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdCancel.SetFocus
    If KeyCode = KEY_ESCAPE Then txtSpouseTermRider.Text = clipboard.GetText()
    If KeyCode = KEY_RETURN Or KeyCode = KEY_ESCAPE Then
       If Val(txtSpouseTermRider) = 0 Then
          If optSpousePremiumChoice(0).Value = True Then
             optSpousePremiumChoice(0).SetFocus
          ElseIf optSpousePremiumChoice(1).Value = True Then
             optSpousePremiumChoice(1).SetFocus
          Else
             optSpousePremiumChoice(2).SetFocus
          End If
       Else
          txtSpouseRiderYearEnd.Enabled = True
          cboSpouseRiderYrAgeEnd.Enabled = True
          txtSpouseRiderYearEnd.SetFocus
       End If
    End If

End Sub

Sub txtSpouseTermRider_LostFocus ()

    txtSpouseTermRider.ForeColor = &H80000008
    txtSpouseTermRider.BackColor = &H80000005
    If Val(txtSpouseTermRider.Text) = 0 Then
       txtSpouseTermRider.Text = "_______"
       txtSpouseRiderYearEnd.Text = "__"
       txtSpouseRiderYearEnd.Enabled = False
       cboSpouseRiderYrAgeEnd.Enabled = False
       txtSpouseTermRider.Text = "_______"
    Else
       txtSpouseRiderYearEnd.Enabled = True
       cboSpouseRiderYrAgeEnd.Enabled = True
    End If
    
    If Val(txtSpouseTermRider.Text) > 0 Then
       msgline$ = ""
       If Val(txtSpouseTermRider.Text) < 15000 Or Val(txtSpouseTermRider.Text) > 5000000 Then
          msgline$ = "Face Amount must be between $15,000 and $5,000,000, inclusive"
       End If
       If Val(txtSpouseTermRider.Text) < 20000 And Val(txtSpouseAge.Text) < 61 Then
          msgline$ = "Face Amount must be at least $20,000 for a Spouse age " & Format$(Val(txtSpouseAge.Text), "#0")
       End If
       If Val(txtSpouseTermRider.Text) < 25000 And Val(txtSpouseAge.Text) < 51 Then
          msgline$ = "Face Amount must be at least $25,000 for a Spouse age " & Format$(Val(txtSpouseAge.Text), "#0")
       End If
       If msgline$ <> "" Then
          MsgBox msgline$
          txtSpouseTermRider.Text = "_______"
          txtSpouseTermRider.SetFocus
          Exit Sub
       End If
    End If

End Sub

