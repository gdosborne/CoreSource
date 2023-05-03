VERSION 2.00
Begin Form frmLIFESV 
   Caption         =   "Life Savings II Principal Insured Illustration Parameters "
   ClientHeight    =   6915
   ClientLeft      =   60
   ClientTop       =   420
   ClientWidth     =   9585
   ClipControls    =   0   'False
   Height          =   7395
   Icon            =   LIFESAVE.FRX:0000
   Left            =   0
   LinkTopic       =   "Form1"
   ScaleHeight     =   6915
   ScaleWidth      =   9585
   Top             =   0
   Width           =   9705
   Begin TextBox txtClientCurrentInt 
      Height          =   285
      Left            =   2160
      MaxLength       =   6
      TabIndex        =   9
      Text            =   " "
      Top             =   1200
      Width           =   975
   End
   Begin Frame fraRiders 
      Caption         =   "Spouse, Children and Additional Insureds"
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   855
      Left            =   120
      TabIndex        =   53
      Top             =   4560
      Width           =   9255
      Begin SSCommand cmdSpouse 
         BevelWidth      =   4
         Caption         =   "View &SPOUSE"
         Enabled         =   0   'False
         Height          =   495
         Left            =   4800
         TabIndex        =   25
         Top             =   240
         Visible         =   0   'False
         Width           =   1815
      End
      Begin SSCheck chkSpouseClient 
         Caption         =   "Create a second illustration with Spouse as Client"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Left            =   120
         TabIndex        =   24
         Top             =   360
         Width           =   4335
      End
      Begin SSCommand cmdRiders 
         BevelWidth      =   4
         Caption         =   "INSURED &RIDERS"
         Height          =   495
         Left            =   6960
         TabIndex        =   26
         Top             =   240
         Width           =   2055
      End
   End
   Begin Frame fraPremium 
      Caption         =   "Illustrate Using:"
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   1455
      Left            =   120
      TabIndex        =   49
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
      Begin SSOption optPremiumChoice 
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
      Begin SSOption optPremiumChoice 
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
      Begin SSOption optPremiumChoice 
         Caption         =   "Guideline Premium"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   0
         Left            =   120
         TabIndex        =   18
         Top             =   360
         Value           =   -1  'True
         Width           =   2055
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
         TabIndex        =   52
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
         TabIndex        =   51
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
         TabIndex        =   50
         Top             =   240
         Width           =   2175
      End
   End
   Begin TextBox txtClientState 
      Height          =   285
      Left            =   8880
      MaxLength       =   2
      TabIndex        =   6
      Top             =   480
      Width           =   495
   End
   Begin Frame frmPrinOptions 
      Caption         =   "Additional Features and Riders for Client"
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   1335
      Left            =   120
      TabIndex        =   43
      Top             =   1560
      Width           =   9255
      Begin ComboBox cboClientRiderYrAgeEnd 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Left            =   8040
         Style           =   2  'Dropdown List
         TabIndex        =   17
         Top             =   960
         Width           =   855
      End
      Begin MaskEdBox txtClientRiderYearEnd 
         Height          =   285
         Left            =   7680
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   "_"
         TabIndex        =   16
         Top             =   960
         Width           =   375
      End
      Begin MaskEdBox txtClientTermRider 
         Height          =   285
         Left            =   6240
         Mask            =   "#######"
         MaxLength       =   7
         PromptChar      =   "_"
         TabIndex        =   15
         Top             =   960
         Width           =   975
      End
      Begin ComboBox cboClientWPD 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   300
         Left            =   1800
         Style           =   2  'Dropdown List
         TabIndex        =   12
         Top             =   960
         Width           =   975
      End
      Begin ComboBox cboClientCOLA 
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
      Begin ComboBox cboClientGPO 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   300
         Left            =   3240
         Style           =   2  'Dropdown List
         TabIndex        =   13
         Top             =   960
         Width           =   1095
      End
      Begin ComboBox cboClientSubStd 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   300
         Left            =   360
         Style           =   2  'Dropdown List
         TabIndex        =   11
         Top             =   960
         Width           =   975
      End
      Begin Label lblRider 
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
         TabIndex        =   56
         Top             =   600
         Width           =   1215
      End
      Begin Label lblRider 
         Alignment       =   2  'Center
         Caption         =   " Death Benefit  Amount"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Index           =   1
         Left            =   6240
         TabIndex        =   57
         Top             =   600
         Width           =   975
      End
      Begin Label lblRider 
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
         TabIndex        =   48
         Top             =   360
         Width           =   2655
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
         TabIndex        =   46
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
         Height          =   615
         Left            =   4800
         TabIndex        =   47
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
         TabIndex        =   45
         Top             =   360
         Width           =   1095
      End
      Begin Label lblPrinSubStd 
         Alignment       =   2  'Center
         Caption         =   "Table Rating (Substandard)"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Left            =   240
         TabIndex        =   44
         Top             =   360
         Width           =   1095
      End
   End
   Begin ComboBox cboClientOption 
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   8.25
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   300
      Left            =   6600
      Style           =   2  'Dropdown List
      TabIndex        =   10
      Top             =   1200
      Width           =   2775
   End
   Begin MaskEdBox txtPhoneContact 
      Height          =   285
      Left            =   2880
      Mask            =   "(###) ###-#### Ext(#####)"
      MaxLength       =   25
      PromptChar      =   "_"
      TabIndex        =   28
      Top             =   5760
      Width           =   2775
   End
   Begin TextBox txtPreparedBy 
      BorderStyle     =   0  'None
      Height          =   225
      Left            =   1560
      MaxLength       =   40
      TabIndex        =   27
      Top             =   5520
      Width           =   5535
   End
   Begin ComboBox cboClientPlan 
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   8.25
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   315
      Left            =   1440
      Style           =   2  'Dropdown List
      TabIndex        =   7
      Top             =   840
      Width           =   1695
   End
   Begin MaskEdBox txtClientFace 
      Height          =   285
      Left            =   8400
      Mask            =   "#######"
      MaxLength       =   7
      PromptChar      =   "_"
      TabIndex        =   8
      Top             =   840
      Width           =   975
   End
   Begin TextBox txtClientSex 
      Height          =   285
      Left            =   6600
      MaxLength       =   1
      TabIndex        =   5
      Top             =   480
      Width           =   375
   End
   Begin MaskEdBox txtClientBirth 
      Height          =   285
      Left            =   3480
      Mask            =   "##/##/####"
      MaxLength       =   10
      PromptChar      =   "_"
      TabIndex        =   4
      Top             =   480
      Width           =   1335
   End
   Begin TextBox txtClientAge 
      Height          =   285
      Left            =   1440
      MaxLength       =   2
      TabIndex        =   3
      Text            =   " "
      Top             =   480
      Width           =   375
   End
   Begin TextBox txtClientLastName 
      Height          =   285
      Left            =   5280
      MaxLength       =   20
      TabIndex        =   2
      Text            =   " "
      Top             =   120
      Width           =   4095
   End
   Begin TextBox txtClientMidInit 
      Height          =   285
      Left            =   4920
      MaxLength       =   1
      TabIndex        =   1
      Text            =   " "
      Top             =   120
      Width           =   255
   End
   Begin TextBox txtClientFirstName 
      Height          =   285
      Left            =   1440
      MaxLength       =   20
      TabIndex        =   0
      Top             =   120
      Width           =   3375
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
      Height          =   615
      Left            =   8160
      TabIndex        =   33
      Top             =   6120
      Width           =   1215
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
      Height          =   615
      Left            =   6600
      TabIndex        =   32
      Top             =   6120
      Width           =   1215
   End
   Begin SSCommand cmdClear 
      BevelWidth      =   4
      Caption         =   "&CLEAR"
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   615
      Left            =   4680
      TabIndex        =   31
      Top             =   6120
      Width           =   1575
   End
   Begin SSCommand cmdProduct 
      BevelWidth      =   4
      Caption         =   "PRO&DUCTS"
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   615
      Left            =   2640
      TabIndex        =   30
      Top             =   6120
      Width           =   1695
   End
   Begin SSCommand cmdClientList 
      BevelWidth      =   4
      Caption         =   "CLIE&NT LIST"
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   615
      Left            =   240
      TabIndex        =   29
      Top             =   6120
      Width           =   2055
   End
   Begin Label lblCurrentInt 
      Caption         =   "Current Interest Rate:"
      Height          =   255
      Left            =   120
      TabIndex        =   41
      Top             =   1200
      Width           =   1935
   End
   Begin Label lblState 
      Caption         =   "State of Residence:"
      Height          =   255
      Left            =   7080
      TabIndex        =   38
      Top             =   480
      Width           =   1815
   End
   Begin Label lblContactPhone 
      Caption         =   "Contact Telephone Number:"
      Height          =   255
      Left            =   240
      TabIndex        =   55
      Top             =   5760
      Width           =   2535
   End
   Begin Label lblPreparedBy 
      Caption         =   "Prepared By:"
      Height          =   255
      Left            =   240
      TabIndex        =   54
      Top             =   5520
      Width           =   1215
   End
   Begin Label lblClientFace 
      Caption         =   "Initial Specified Death Benefit:"
      Height          =   255
      Left            =   5280
      TabIndex        =   40
      Top             =   840
      Width           =   2775
   End
   Begin Label lblOptionCode 
      Caption         =   "Option Code:"
      Height          =   255
      Left            =   5280
      TabIndex        =   42
      Top             =   1200
      Width           =   1215
   End
   Begin Label lblPlanCode 
      Caption         =   "Plan Code:"
      Height          =   255
      Left            =   120
      TabIndex        =   39
      Top             =   840
      Width           =   975
   End
   Begin Label lblClientSex 
      Caption         =   "Gender:"
      Height          =   255
      Left            =   5280
      TabIndex        =   37
      Top             =   480
      Width           =   735
   End
   Begin Label lblClientBirth 
      Caption         =   "or Birthdate:"
      Height          =   255
      Left            =   2040
      TabIndex        =   36
      Top             =   480
      Width           =   1095
   End
   Begin Label lblClientAge 
      Caption         =   "Age:"
      Height          =   255
      Left            =   120
      TabIndex        =   35
      Top             =   480
      Width           =   1095
   End
   Begin Label lblClienName 
      Caption         =   "Client Name:"
      Height          =   255
      Left            =   120
      TabIndex        =   34
      Top             =   120
      Width           =   1095
   End
End

Sub cboClientCOLA_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then txtClientTermRider.SetFocus

End Sub

Sub cboClientCOLA_LostFocus ()
    
    If Val(txtClientAge.Text) > 60 Then
       If cboClientCOLA.ListIndex <> 0 Then
          msgline$ = "Cost of Living Rider not allowed; client must be less than age 61"
          MsgBox msgline$
          Exit Sub
       End If
    End If
    
    If cboClientSubStd.ListIndex > 0 Then
       If cboClientCOLA.ListIndex <> 0 Then
          msgline$ = "Cost of Living Rider not allowed if Client has substandard Table Rating"
          MsgBox msgline$
          cboClientCOLA.ListIndex = 0
          Exit Sub
       End If
    End If

End Sub

Sub cboClientGPO_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then cboClientCOLA.SetFocus

End Sub

Sub cboClientGPO_LostFocus ()
    
    If Val(txtClientAge.Text) > 35 Then
       If cboClientGPO.ListIndex <> 0 Then
          msgline$ = "Guaranteed Added Death Benefit Rider not allowed; client must be less than age 36 to add"
          MsgBox msgline$
          cboClientGPO.ListIndex = 0
          Exit Sub
       End If
    End If
    
    If cboClientSubStd.ListIndex > 0 Then
       If cboClientGPO.ListIndex <> 0 Then
          msgline$ = "Guaranteed Added Death Benefit Rider not allowed if Client has substandard Table Rating"
          MsgBox msgline$
          cboClientGPO.ListIndex = 0
          Exit Sub
       End If
    End If

End Sub

Sub cboClientOption_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then cboClientSubStd.SetFocus

End Sub

Sub cboClientPlan_Change ()

    ClientChanged = True

End Sub

Sub cboClientPlan_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then txtClientFace.SetFocus

End Sub

Sub cboClientRiderYrAgeEnd_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then
       If optPremiumChoice(0).Value = True Then
          optPremiumChoice(0).SetFocus
       ElseIf optPremiumChoice(1).Value = True Then
          optPremiumChoice(1).SetFocus
       Else
          optPremiumChoice(2).SetFocus
       End If
    End If

End Sub

Sub cboClientRiderYrAgeEnd_LostFocus ()

    Dim TempAge As Integer
   
    If txtClientTermRider.Text = "_______" Then Exit Sub
    TempAge = Val(txtClientAge.Text)
    If txtClientRiderYearEnd.Text = "__" Then
       If cboClientRiderYrAgeEnd.ListIndex = 1 Then
          txtClientRiderYearEnd.Text = 95
       Else
          txtClientRiderYearEnd.Text = 95 - TempAge
       End If
    End If
    Select Case cboClientRiderYrAgeEnd.ListIndex
       Case 1
          If Val(txtClientRiderYearEnd.Text) <= TempAge Then
             msgline$ = "Age for Rider must be greater than Client Issue Age"
             MsgBox msgline$
             txtClientRiderYearEnd.SetFocus
             Exit Sub
          End If
       Case Else
          If Val(txtClientRiderYearEnd.Text) > 95 - TempAge Then
             msgline$ = "Years for Rider cannot be greater than Client Age 95"
             MsgBox msgline$
             txtClientRiderYearEnd.SetFocus
             Exit Sub
          End If
    End Select
    
End Sub

Sub cboClientSubStd_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then cboClientWPD.SetFocus

End Sub

Sub cboClientSubStd_LostFocus ()
    
    If cboClientSubStd.ListIndex > 0 Then
       cboClientWPD.ListIndex = 0    'not allowed if substd
       cboClientGPO.ListIndex = 0
       cboClientCOLA.ListIndex = 0
    End If

End Sub

Sub cboClientWPD_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then cboClientGPO.SetFocus

End Sub

Sub cboClientWPD_LostFocus ()
    
    If Val(txtClientAge.Text) > 55 Then
       If cboClientWPD.ListIndex <> 0 Then
          msgline$ = "Disability Waiver of Cost Rider not allowed; client must be less than age 56"
          MsgBox msgline$
          cboClientWPD.ListIndex = 0
          Exit Sub
       End If
    End If

    If Val(txtClientAge.Text) < 15 Then
       If cboClientWPD.ListIndex <> 0 Then
          msgline$ = "Disability Waiver of Cost Rider not allowed; client must be greater than age 14"
          MsgBox msgline$
          cboClientWPD.ListIndex = 0
          Exit Sub
       End If
    End If

    If cboClientSubStd.ListIndex > 0 Then
       If cboClientWPD.ListIndex <> 0 Then
          msgline$ = "Disability Waiver of Cost Rider not allowed if Client has substandard Table Rating"
          MsgBox msgline$
          cboClientWPD.ListIndex = 0
          Exit Sub
       End If
    End If

End Sub

Sub chkSpouseClient_Click (Value As Integer)

    If chkSpouseClient.Value = True Then
       cmdSpouse.Enabled = True
       cmdSpouse.Visible = True
    Else
       cmdSpouse.Enabled = False
       cmdSpouse.Visible = False
    End If

End Sub

Sub cmdClear_Click ()

    Dim s As Integer
    Dim t As Integer

    If ClientChanged = True And AgtInfo.Record <> 0 Then
       Call ClientSave
    End If
    frmLIFESV.txtClientFirstName.Text = ""
    frmLIFESV.txtClientMidInit.Text = ""
    frmLIFESV.txtClientLastName.Text = ""
    frmLIFESV.txtClientState.Text = ""
    frmLIFESV.txtClientSex.Text = ""
    frmLIFESV.cboClientPlan.ListIndex = 0
    frmLIFESV.txtClientAge.Text = "  "
    frmLIFESV.txtClientBirth.Text = "__/__/____"
    frmLIFESV.txtClientFace.Text = "_______"
    frmLIFESV.txtClientCurrentInt.Text = "      "
    frmLIFESV.cboClientOption.ListIndex = 0
    frmLIFESV.cboClientSubStd.ListIndex = 0
    frmLIFESV.cboClientWPD.ListIndex = 0
    frmLIFESV.cboClientGPO.ListIndex = 0
    frmLIFESV.cboClientCOLA.ListIndex = 0
    frmLIFESV.txtClientTermRider.Text = "_______"
    frmLIFESV.txtClientRiderYearEnd.Text = "__"
    frmLIFESV.cboClientRiderYrAgeEnd.ListIndex = 0
    
    For s = 0 To 1
      LSClient(s).Mode = " "
      LSClient(s).PlannedPrem = 0
      LSClient(s).YearsToPay = 0
      LSClient(s).LumpSum = 0
      LSClient(s).SpouseRider = 0
      LSClient(s).ChildRider = 0
      For t = 1 To 9
        LSClient(s).InsuredRider(t) = 0
      Next t
      LSClient(s).FutureChanges = 0
      LSClient(s).FutureWD = 0
      LSClient(s).IllusType = 0
      For t = 1 To 4
        LSClient(s).HighAges(t) = 0
      Next t
    Next s

    For s = 1 To 5
      LSFuture(s).DB_Age = 0
      LSFuture(s).Prem_Age = 0
      LSFuture(s).Int_Age = 0
      LSFuture(s).Opt_Age = 0
      LSFutureWD(s).WD_Age = 0
      LSFutureWD(s).Loan_Age = 0
      LSFutureWD(s).LoanPay_Age = 0
    Next s
    AgeChanged = 0

End Sub

Sub cmdClientList_Click ()
    
    Dim Msg As String
    Dim FileNum As Integer

    On Error GoTo ErrClientList
    
    No_Clients = False
    
    If UCase$(Left$(ClientFile, 4)) <> "CLNT" Then
       Msg = "No Clients on file"
       MsgBox Msg, 64
       No_Clients = True
       Exit Sub
    End If
    
    FileNum = FreeFile
    Open ClientFile For Random As #FileNum Len = Len(ClientInfo)

    'Calculate Number of Records in File
    NumOfRec% = LOF(FileNum) \ Len(ClientInfo)

    'Retrieve records and load into list box
    For i% = 1 To NumOfRec%
      Get #FileNum, i%, ClientInfo
      If Left$(ClientInfo.LastName, 1) <> "*" Then
         tmpname$ = RTrim$(ClientInfo.LastName) + "," + RTrim$(ClientInfo.FirstName) + " " + ClientInfo.Initial + "-" + RTrim$(ClientInfo.City) + ",#" + Trim$(Right$(Str$(ClientInfo.ClientNumber), 5))
         frmCLLIST.List1.AddItem tmpname$
      End If
    Next i%
    Close #FileNum
    If frmCLLIST.List1.ListCount > 0 Then
       frmCLLIST.List1.Selected(0) = True
    Else
       Msg = "No Clients on file"
       MsgBox Msg, 64
       No_Clients = True
       Exit Sub
    End If
    
    Unload frmLIFESV
    frmCLLIST.Show
    Exit Sub

ErrClientList:
    Select Case Err
        Case 364
            Resume Next
        Case Else
            Msg = "Err: " & Error$(Err) & "," & Str$(Err) & "Client List"
            MsgBox Msg, 64
            Exit Sub
    End Select

End Sub

Sub cmdExit_Click ()

    Dim ListItem As String
    Dim CurrWnd As Integer

    If ClientChanged = True And AgtInfo.Record <> 0 Then
       Call ClientSave
       If ClientSave_ReturnCode = 3 Then Exit Sub
    End If

    CurrWnd = GetWindow(frmLIFESV.hWnd, GW_HWNDFIRST)
    Do
      Length = GetWindowTextLength(CurrWnd)
      ListItem = Space$(Length + 1)
      Length = GetWindowText(CurrWnd, ListItem, Length + 1)
      If Mid$(ListItem, 1, 17) = "American Republic" Then
         TempWnd = ShowWindow(CurrWnd, SW_SHOWNORMAL)
         CurrWnd = SetFocusAPI(CurrWnd)
         Exit Do
      End If
      CurrWnd = GetWindow(CurrWnd, GW_HWNDNEXT)
    Loop While CurrWnd <> 0
    Unload frmLIFESV
    End

End Sub

Sub cmdFuture_Click ()
    
    Call Set_Client_Illus_Parameters
    frmLIFESV.Hide
    frmLSCHANGE.Show 0

End Sub

Sub cmdHelp_Click ()
    
    Dim i As Integer
    
    i = WinHelp(frmLIFESV.hWnd, "life\lifesave.hlp" + Chr$(0), 1&, 1&)

End Sub

Sub cmdIllustrate_Click ()

    LifeIllusType = 1
   
    s = SpouseClient
    Call Set_Client_Illus_Parameters

    i = ReadyToIllustrate()
    If i = False Then
       msgtext$ = "Cannot Illustrate until valid Age, Sex, Death Benefit and Interest Rate are input"
       MsgBox msgtext$
       Exit Sub
    End If

    frmLIFESV.MousePointer = 11
    frmLSPREM.txtPremCurrentInt.Text = frmLIFESV.txtClientCurrentInt.Text
    frmLSPREM.cboPremiumMode.ListIndex = 3  'Annual mode assumed
    frmLSPREM.txtModalPremium.Text = 0
    frmLSPREM.cboYearsPay.Text = Format$(95 - Val(frmLIFESV.txtClientAge.Text), "#0") & " Years"
    frmLSPREM.txtPremiumLump = ""
    
    Call Set_Premium_Illus_Parameters
    Call Calculate_Initial_Premiums
    
    If frmLIFESV.optPremiumChoice(0).Value = True Then
       frmLSPREM.txtModalPremium.Text = Format$(CalcPrem.GuideAnnual, "000000.00")
    ElseIf frmLIFESV.optPremiumChoice(1).Value = True Then
       frmLSPREM.txtModalPremium.Text = Format$(CalcPrem.TotTarg, "000000.00")
    ElseIf frmLIFESV.optPremiumChoice(2).Value = True Then
       frmLSPREM.txtModalPremium.Text = Format$(CalcPrem.TotMin, "000000.00")
    End If
    
    Call Set_Premium_Illus_Parameters
    Call Calculate_Full_LS_Ledger
    frmLIFESV.MousePointer = 0
    
    frmLSILLTYP.Show 0

End Sub

Sub cmdPremium_Click ()

    Call Set_Client_Illus_Parameters
    frmLIFESV.Hide
    frmLSPREM.Show 0

End Sub

Sub cmdProduct_Click ()

    Dim ListItem As String
    Dim CurrWnd As Integer
    Dim ChildWnd As Integer
    Dim ParentWnd As Integer

    CurrWnd = GetWindow(frmLIFESV.hWnd, GW_HWNDFIRST)
    Do
      Length = GetWindowTextLength(CurrWnd)
      ListItem = Space$(Length + 1)
      Length = GetWindowText(CurrWnd, ListItem, Length + 1)
      If Mid$(ListItem, 1, 17) = "American Republic" Then
         TempWnd = ShowWindow(CurrWnd, SW_SHOWNORMAL)
         CurrWnd = SetFocusAPI(CurrWnd)
         SendKeys "%P", False
         Exit Do
      End If
      CurrWnd = GetWindow(CurrWnd, GW_HWNDNEXT)
    Loop While CurrWnd <> 0
    Unload frmLIFESV
    End

End Sub

Sub cmdRiders_Click ()

    Call Set_Client_Illus_Parameters
    frmLIFESV.Hide
    frmLSINSD.Show 0

End Sub

Sub cmdSpouse_Click ()

    Call Set_Client_Illus_Parameters
    frmLIFESV.Hide
    frmLSSPOUSE.Show 0

End Sub

Sub Form_Load ()

    If CallFromMain <> "NO" Then
       CommandString = Command$
       Call Parse_Command_Argument(CommandString, AgtInfo.Record, ClientInfo.ClientNumber)
    End If

    RetStr$ = GetAnyIniStr("Americare", "LFont", "AMRICARE.INI")
    If Trim$(RetStr$) <> "" Then defPrintFont = RetStr$
    RetStr$ = GetAnyIniStr("Americare", "LFontSiz", "AMRICARE.INI")
    If Trim$(RetStr$) <> "" Then
       defFontSiz = Val(RetStr$) + .24
    Else
       defFontSiz = 12
    End If
    
    Call Open_LS_RateFiles
    
    LSClient(0).ClientName = ""
    LSClient(1).ClientName = ""

    SpouseClient = 0
    cboClientPlan.AddItem "NonSmoker"
    cboClientPlan.AddItem "Smoker"
    cboClientPlan.ListIndex = 0
    cboClientOption.AddItem "1 - Level Death Benefit"
    cboClientOption.AddItem "2 - Increasing Death Benefit"
    cboClientOption.ListIndex = 0
    cboClientSubStd.AddItem "None"
    cboClientSubStd.AddItem "1.25"
    cboClientSubStd.AddItem "1.50"
    cboClientSubStd.AddItem "1.75"
    cboClientSubStd.AddItem "2.00"
    cboClientSubStd.AddItem "2.25"
    cboClientSubStd.AddItem "2.50"
    cboClientSubStd.AddItem "2.75"
    cboClientSubStd.AddItem "3.00"
    cboClientSubStd.AddItem "3.25"
    cboClientSubStd.AddItem "3.50"
    cboClientSubStd.AddItem "3.75"
    cboClientSubStd.AddItem "4.00"
    cboClientSubStd.AddItem "4.25"
    cboClientSubStd.AddItem "4.50"
    cboClientSubStd.AddItem "4.75"
    cboClientSubStd.AddItem "5.00"
    cboClientSubStd.ListIndex = 0
    cboClientRiderYrAgeEnd.AddItem "Year"
    cboClientRiderYrAgeEnd.AddItem "Age"
    cboClientRiderYrAgeEnd.ListIndex = 0
    cboClientGPO.AddItem "None"
    cboClientGPO.AddItem "5,000"
    cboClientGPO.AddItem "6,000"
    cboClientGPO.AddItem "7,000"
    cboClientGPO.AddItem "8,000"
    cboClientGPO.AddItem "9,000"
    cboClientGPO.AddItem "10,000"
    cboClientGPO.AddItem "11,000"
    cboClientGPO.AddItem "12,000"
    cboClientGPO.AddItem "13,000"
    cboClientGPO.AddItem "14,000"
    cboClientGPO.AddItem "15,000"
    cboClientGPO.AddItem "16,000"
    cboClientGPO.AddItem "17,000"
    cboClientGPO.AddItem "18,000"
    cboClientGPO.AddItem "19,000"
    cboClientGPO.AddItem "20,000"
    cboClientGPO.AddItem "21,000"
    cboClientGPO.AddItem "22,000"
    cboClientGPO.AddItem "23,000"
    cboClientGPO.AddItem "24,000"
    cboClientGPO.AddItem "25,000"
    cboClientGPO.ListIndex = 0
    cboClientCOLA.AddItem "None"
    cboClientCOLA.AddItem "1%"
    cboClientCOLA.AddItem "2%"
    cboClientCOLA.AddItem "3%"
    cboClientCOLA.AddItem "4%"
    cboClientCOLA.AddItem "5%"
    cboClientCOLA.AddItem "6%"
    cboClientCOLA.AddItem "7%"
    cboClientCOLA.AddItem "8%"
    cboClientCOLA.AddItem "9%"
    cboClientCOLA.AddItem "10%"
    cboClientCOLA.ListIndex = 0
    cboClientWPD.AddItem "No"
    cboClientWPD.AddItem "Yes"
    cboClientWPD.ListIndex = 0
    
    Call Load_LS_From_Client

    AgeChanged = 0

End Sub

Sub Form_Unload (Cancel As Integer)

    Dim i As Integer

    CallFromMain = "NO"
    Call Close_LS_RateFiles
    i = WinHelp(frmLIFESV.hWnd, dummy$, 2&, 0&)
    Set frmLIFESV = Nothing
   
End Sub

Sub lblClientAge_Click ()

    AgeChanged = 3

End Sub

Sub txtClientAge_Change ()
 
    ClientChanged = True
    If AgeChanged <> 3 Then AgeChanged = 1

End Sub

Sub txtClientAge_GotFocus ()
 
    clipboard.Clear
    clipboard.SetText Trim$(txtClientAge.Text)
    txtClientAge.Text = ""
    txtClientAge.Text = clipboard.GetText()
    txtClientAge.ForeColor = &H80000005
    txtClientAge.BackColor = &H80000008
    If AgeChanged <> 3 Then AgeChanged = 0

End Sub

Sub txtClientAge_KeyDown (KeyCode As Integer, Shift As Integer)
    
    Select Case (KeyCode)
       Case KEY_HOME, KEY_END
          If Shift = 4 Then
             Call txtClientAge_LostFocus
             If KeyCode = KEY_HOME Then txtClientFirstName.SetFocus
             If KeyCode = KEY_END Then cmdHelp.SetFocus
          End If
       Case KEY_DOWN, KEY_UP, KEY_RETURN, KEY_ESCAPE
          If KeyCode = KEY_RETURN And AgeChanged <> 3 Then AgeChanged = 2
          If KeyCode = KEY_ESCAPE Then
             txtClientAge.Text = clipboard.GetText()
             If AgeChanged <> 3 Then AgeChanged = 2
          End If
          Call txtClientAge_LostFocus
          If KeyCode = KEY_DOWN Then cboClientPlan.SetFocus
          If KeyCode = KEY_UP Then txtClientFirstName.SetFocus
    End Select

End Sub

Sub txtClientAge_KeyPress (KeyAscii As Integer)

    If KeyAscii < 48 Or KeyAscii > 57 Then
       KeyAscii = 0
    Else
       If txtClientAge.Text = clipboard.GetText() Then
          txtClientAge.Text = ""
       End If
    End If

End Sub

Sub txtClientAge_KeyUp (KeyCode As Integer, Shift As Integer)

    If KeyCode > 47 And KeyCode < 58 Then
       If Len(txtClientAge.Text) >= 2 Then
          If AgeChanged <> 3 Then AgeChanged = 2
          Call txtClientAge_LostFocus
       End If
    End If

End Sub

Sub txtClientAge_LostFocus ()

    Dim TempAge As Integer

    If AgeChanged = 0 Or txtClientAge.Text = "" Then GoTo End_AgeProc
    TempAge = Val(txtClientAge.Text)
    msgtext$ = ""
    If TempAge > 70 Then
       msgtext$ = "Issue age must be between less than 71 for Life Savings client"
    End If
    If msgtext$ <> "" Then
       MsgBox msgtext$
       txtClientAge.Text = ""
       txtClientAge.SetFocus
       Exit Sub
    End If
    
    If AgeChanged <> 3 Then
       If Val(txtClientBirth.Text) <> 0 Then
           mm$ = Left$(txtClientBirth.Text, 2)
           dd$ = Mid$(txtClientBirth.Text, 4, 2)
       Else
           mm$ = Str$(Month(Now))
           dd$ = Str$(Day(Now))
       End If
       If Val(mm$) > Month(Now) Or (Val(mm$) = Month(Now) And Val(dd$) > Day(Now)) Then
           yy$ = Str$(Year(Now) - Val(txtClientAge.Text) - 1)
       Else
           yy$ = Str$(Year(Now) - Val(txtClientAge.Text))
       End If
       serialdt& = DateSerial(Val(yy$), Val(mm$), Val(dd$))
       txtClientBirth.Text = Format$(serialdt&, "MM/DD/YYYY")
    End If

    If TempAge > 35 Then cboClientGPO.ListIndex = 0
    If TempAge < 15 Or TempAge > 59 Then cboClientWPD.ListIndex = 0
    If TempAge > 60 Then cboClientCOLA.ListIndex = 0
    If TempAge > 65 Then
       txtClientTermRider.Text = "_______"
       txtClientRiderYearEnd.Text = "__"
       cboClientRiderYrAgeEnd.ListIndex = 0
    End If

End_AgeProc:
    
    If AgeChanged >= 2 Then txtClientBirth.SetFocus
    If Val(txtClientFace.Text) > 0 Or Val(txtClientTermRider.Text) > 0 Then
       Select Case TempAge
          Case 61 To 70
             If Val(txtClientFace.Text) > 0 And Val(txtClientFace.Text) < 15000 Then
                txtClientFace.Text = "15000__"
             End If
             If Val(txtClientTermRider.Text) > 0 And Val(txtClientTermRider.Text) < 15000 Then
                txtClientTermRider.Text = "15000__"
             End If
          Case 51 To 60
             If Val(txtClientFace.Text) > 0 And Val(txtClientFace.Text) < 20000 Then
                txtClientFace.Text = "20000__"
             End If
             If Val(txtClientTermRider.Text) > 0 And Val(txtClientTermRider.Text) < 20000 Then
                txtClientTermRider.Text = "20000__"
             End If
          Case Else
             If Val(txtClientFace.Text) > 0 And Val(txtClientFace.Text) < 25000 Then
                txtClientFace.Text = "25000__"
             End If
             If Val(txtClientTermRider.Text) > 0 And Val(txtClientTermRider.Text) < 25000 Then
                txtClientTermRider.Text = "25000__"
             End If
       End Select
    End If
    If Val(txtClientRiderYearEnd.Text) > 0 Then
       txtClientRiderYearEnd.Text = Right$(Str$(95 - Val(txtClientAge.Text)), 2)
       cboClientRiderYrAgeEnd.ListIndex = 0
    End If
    txtClientAge.ForeColor = &H80000008
    txtClientAge.BackColor = &H80000005

End Sub

Sub txtClientBirth_Change ()
 
    ClientChanged = True
    BirthChanged = 1

End Sub

Sub txtClientBirth_GotFocus ()
 
    clipboard.Clear
    clipboard.SetText Trim$(txtClientBirth.Text)
    txtClientBirth.Text = "__/__/____"
    txtClientBirth.Text = clipboard.GetText()
    txtClientBirth.ForeColor = &H80000005
    txtClientBirth.BackColor = &H80000008
    BirthChanged = 0

End Sub

Sub txtClientBirth_KeyDown (KeyCode As Integer, Shift As Integer)

    Select Case (KeyCode)
       Case KEY_HOME, KEY_END
          If Shift = 4 Then
             Call txtClientBirth_LostFocus
             If KeyCode = KEY_HOME Then txtClientFirstName.SetFocus
             If KeyCode = KEY_END Then cmdHelp.SetFocus
          End If
       Case KEY_DOWN, KEY_UP, KEY_RETURN, KEY_ESCAPE
          If KeyCode = KEY_RETURN Then BirthChanged = 2
          If KeyCode = KEY_ESCAPE Then
             txtClientBirth.Text = clipboard.GetText()
             BirthChanged = 2
          End If
          Call txtClientBirth_LostFocus
          If KeyCode = KEY_DOWN Then cboClientPlan.SetFocus
          If KeyCode = KEY_UP Then txtClientFirstName.SetFocus
    End Select

End Sub

Sub txtClientBirth_KeyUp (KeyCode As Integer, Shift As Integer)
       
    If KeyCode > 47 And KeyCode < 58 Then
       If Mid$(txtClientBirth.Text, 10, 1) <> "_" And txtClientBirth.Text <> clipboard.GetText() Then
          BirthChanged = 2
          Call txtClientBirth_LostFocus
       End If
    End If

End Sub

Sub txtClientBirth_LostFocus ()

    If BirthChanged = 0 Or AgeChanged = 3 Or txtClientBirth.Text = "__/__/____" Then GoTo End_BirthProc
    tempbirth$ = txtClientBirth.Text
    Call Verify_Life_Birth(retcode%, tempbirth$)
    If retcode% = False Then
       txtClientBirth.Text = "__/__/____"
       msgtext$ = "Invalid birthdate"
       MsgBox msgtext$
       txtClientBirth.SetFocus
       Exit Sub
    End If
    If Left$(txtClientBirth.Text, 2) <> "__" Then
       mm$ = Left$(txtClientBirth.Text, 2)
       dd$ = Mid$(txtClientBirth.Text, 4, 2)
       yy$ = Right$(txtClientBirth.Text, 4)
       If Left$(txtClientAge.Text, 2) <> "  " Then
          Xyy$ = Str$(Year(Now) - Val(txtClientAge.Text))
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
          txtClientBirth.Text = "__/__/____"
          txtClientBirth.SetFocus
          Exit Sub
       End If
    End If

    If txtClientAge.Text <> Mid$(Str$(cage%), 2, 2) Then
       txtClientAge.Text = Mid$(Str$(cage%), 2, 2)
    End If

End_BirthProc:
    
    If BirthChanged = 2 Then txtClientSex.SetFocus
    txtClientBirth.ForeColor = &H80000008
    txtClientBirth.BackColor = &H80000005

End Sub

Sub txtClientCurrentInt_GotFocus ()
    
    clipboard.Clear
    clipboard.SetText Trim$(txtClientCurrentInt.Text)
    txtClientCurrentInt.Text = ""
    txtClientCurrentInt.Text = clipboard.GetText()
    txtClientCurrentInt.ForeColor = &H80000005
    txtClientCurrentInt.BackColor = &H80000008

End Sub

Sub txtClientCurrentInt_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then txtClientFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdHelp.SetFocus
    If KeyCode = KEY_RETURN Then cboClientOption.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtClientCurrentInt.Text = clipboard.GetText()
       cboClientOption.SetFocus
    End If

End Sub

Sub txtClientCurrentInt_KeyPress (KeyAscii As Integer)

    If (KeyAscii < 48 Or KeyAscii > 57) And KeyAscii <> 46 Then KeyAscii = 0

End Sub

Sub txtClientCurrentInt_LostFocus ()
    
    txtClientCurrentInt.ForeColor = &H80000008
    txtClientCurrentInt.BackColor = &H80000005
    If Val(txtClientCurrentInt.Text) > 0 And (Val(txtClientCurrentInt.Text) < 4# Or Val(txtClientCurrentInt.Text) > 20#) Then
       msgline$ = "Current Interest Amount must be between 4.00% and 20.00%, inclusive"
       MsgBox msgline$
       txtClientCurrentInt.Text = ""
       txtClientCurrentInt.SetFocus
       Exit Sub
    End If
    tempint! = Val(txtClientCurrentInt.Text)
    If tempint! > 0 Then txtClientCurrentInt.Text = Format$(tempint!, "00.000")

End Sub

Sub txtClientFace_GotFocus ()
    
    clipboard.Clear
    clipboard.SetText Trim$(txtClientFace.Text)
    txtClientFace.Text = "0000000"
    txtClientFace.Text = clipboard.GetText()
    txtClientFace.ForeColor = &H80000005
    txtClientFace.BackColor = &H80000008

End Sub

Sub txtClientFace_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then txtClientFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdHelp.SetFocus
    If KeyCode = KEY_RETURN Then txtClientCurrentInt.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtClientFace.Text = clipboard.GetText()
       txtClientCurrentInt.SetFocus
    End If

End Sub

Sub txtClientFace_LostFocus ()

    txtClientFace.ForeColor = &H80000008
    txtClientFace.BackColor = &H80000005
    If Val(txtClientFace.Text) > 0 Then
       msgline$ = ""
       If Val(txtClientFace.Text) < 15000 Or Val(txtClientFace.Text) > 9999999 Then
          msgline$ = "Face Amount must be between $15,000 and $9,999,999, inclusive"
       End If
       If Val(txtClientFace.Text) < 20000 And Val(txtClientAge.Text) < 61 Then
          msgline$ = "Face Amount must be at least $20,000 for a Client age " & Format$(Val(txtClientAge.Text), "#0")
       End If
       If Val(txtClientFace.Text) < 25000 And Val(txtClientAge.Text) < 51 Then
          msgline$ = "Face Amount must be at least $25,000 for a Client age " & Format$(Val(txtClientAge.Text), "#0")
       End If
       If msgline$ <> "" Then
          MsgBox msgline$
          txtClientFace.Text = "_______"
          txtClientFace.SetFocus
          Exit Sub
       End If
    End If

End Sub

Sub txtClientFirstName_Change ()
    
    If txtClientFirstName.Text <> clipboard.GetText() Then
       ClientChanged = True
    Else
       ClientChanged = False
    End If

End Sub

Sub txtClientFirstName_GotFocus ()
    
    clipboard.Clear
    clipboard.SetText Trim$(txtClientFirstName.Text)
    txtClientFirstName.Text = ""
    txtClientFirstName.Text = clipboard.GetText()
    txtClientFirstName.ForeColor = &H80000005
    txtClientFirstName.BackColor = &H80000008

End Sub

Sub txtClientFirstName_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_END And Shift = 4 Then cmdHelp.SetFocus
    If KeyCode = KEY_DOWN Then txtClientAge.SetFocus
    If KeyCode = KEY_ESCAPE Then txtClientFirstName.Text = clipboard.GetText()
    If KeyCode = KEY_RETURN Or KeyCode = KEY_ESCAPE Then
       onechar$ = Left$(txtClientFirstName, 1)
       If onechar$ = "" Or onechar$ = " " Then
          txtClientFirstName.Text = "Client"
          txtClientMidInit.Text = ""
          txtClientLastName.Text = ""
          txtClientAge.SetFocus
       Else
          onechar$ = UCase(onechar$)
          txtClientFirstName.Text = onechar$ & Mid$(txtClientFirstName.Text, 2)
          txtClientMidInit.SetFocus
       End If
    End If

End Sub

Sub txtClientFirstName_LostFocus ()

    txtClientFirstName.ForeColor = &H80000008
    txtClientFirstName.BackColor = &H80000005
     
End Sub

Sub txtClientLastName_Change ()
 
    If txtClientLastName.Text <> clipboard.GetText() Then
       ClientChanged = True
    Else
       ClientChanged = False
    End If

End Sub

Sub txtClientLastName_GotFocus ()

    clipboard.Clear
    clipboard.SetText Trim$(txtClientLastName.Text)
    txtClientLastName.Text = ""
    txtClientLastName.Text = clipboard.GetText()
    txtClientLastName.ForeColor = &H80000005
    txtClientLastName.BackColor = &H80000008

End Sub

Sub txtClientLastName_KeyDown (KeyCode As Integer, Shift As Integer)
     
    If KeyCode = KEY_HOME And Shift = 4 Then txtClientFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdHelp.SetFocus
    If KeyCode = KEY_DOWN Then txtClientAge.SetFocus
    If KeyCode = KEY_RETURN Then txtClientAge.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtClientLastName.Text = clipboard.GetText()
       txtClientAge.SetFocus
    End If

End Sub

Sub txtClientLastName_LostFocus ()
    
    txtClientLastName.ForeColor = &H80000008
    txtClientLastName.BackColor = &H80000005
    onechar$ = Left$(txtClientLastName, 1)
    onechar$ = UCase(onechar$)
    txtClientLastName.Text = onechar$ & Mid$(txtClientLastName.Text, 2)

End Sub

Sub txtClientMidInit_GotFocus ()
    
    clipboard.Clear
    clipboard.SetText Trim$(txtClientMidInit.Text)
    txtClientMidInit.Text = ""
    txtClientMidInit.Text = clipboard.GetText()
    txtClientMidInit.ForeColor = &H80000005
    txtClientMidInit.BackColor = &H80000008

End Sub

Sub txtClientMidInit_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then txtClientFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdHelp.SetFocus
    If KeyCode = KEY_DOWN Then txtClientAge.SetFocus
    If KeyCode = KEY_RETURN Then txtClientLastName.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtClientMidInit.Text = clipboard.GetText()
       txtClientLastName.SetFocus
    End If

End Sub

Sub txtClientMidInit_LostFocus ()
     
    txtClientMidInit.ForeColor = &H80000008
    txtClientMidInit.BackColor = &H80000005
    txtClientMidInit.Text = UCase$(txtClientMidInit.Text)

End Sub

Sub txtClientRiderYearEnd_GotFocus ()
    
    clipboard.Clear
    clipboard.SetText Trim$(txtClientRiderYearEnd.Text)
    txtClientRiderYearEnd.Text = "__"
    If Val(clipboard.GetText()) <> 0 Then
       txtClientRiderYearEnd.Text = clipboard.GetText()
    End If
    txtClientRiderYearEnd.ForeColor = &H80000005
    txtClientRiderYearEnd.BackColor = &H80000008

End Sub

Sub txtClientRiderYearEnd_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then txtClientFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdHelp.SetFocus
    If KeyCode = KEY_RETURN Then cboClientRiderYrAgeEnd.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtClientRiderYearEnd.Text = clipboard.GetText()
       cboClientRiderYrAgeEnd.SetFocus
    End If

End Sub

Sub txtClientRiderYearEnd_LostFocus ()
    
    txtClientRiderYearEnd.ForeColor = &H80000008
    txtClientRiderYearEnd.BackColor = &H80000005

End Sub

Sub txtClientSex_Change ()
 
    ClientChanged = True

End Sub

Sub txtClientSex_GotFocus ()
    
    clipboard.Clear
    clipboard.SetText Trim$(txtClientSex.Text)
    txtClientSex.Text = ""
    txtClientSex.Text = clipboard.GetText()
    txtClientSex.ForeColor = &H80000005
    txtClientSex.BackColor = &H80000008

End Sub

Sub txtClientSex_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then txtClientFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdHelp.SetFocus
    If KeyCode = KEY_UP Then txtClientFirstName.SetFocus
    If KeyCode = KEY_DOWN Then cboClientPlan.SetFocus
    If KeyCode = KEY_RETURN Then txtClientState.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtClientSex.Text = clipboard.GetText()
       txtClientState.SetFocus
    End If

End Sub

Sub txtClientSex_KeyPress (KeyAscii As Integer)
     
    Select Case (KeyAscii)
       Case 32, 70, 77, 102, 109
          If txtClientSex.Text = clipboard.GetText() Then
             txtClientSex.Text = ""
             txtClientState.SetFocus
          End If
       Case Else
          KeyAscii = 0
    End Select

End Sub

Sub txtClientSex_LostFocus ()
    
    txtClientSex.ForeColor = &H80000008
    txtClientSex.BackColor = &H80000005
    txtClientSex.Text = UCase$(txtClientSex.Text)

End Sub

Sub txtClientState_GotFocus ()
    
    clipboard.Clear
    clipboard.SetText Trim$(txtClientState.Text)
    txtClientState.Text = ""
    txtClientState.Text = clipboard.GetText()
    txtClientState.ForeColor = &H80000005
    txtClientState.BackColor = &H80000008

End Sub

Sub txtClientState_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then txtClientFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdHelp.SetFocus
    If KeyCode = KEY_UP Then txtClientFirstName.SetFocus
    If KeyCode = KEY_DOWN Then cboClientPlan.SetFocus
    If KeyCode = KEY_RETURN Then cboClientPlan.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtClientState.Text = clipboard.GetText()
       cboClientPlan.SetFocus
    End If

End Sub

Sub txtClientState_KeyPress (KeyAscii As Integer)
    
    If KeyAscii = 32 Or (KeyAscii > 64 And KeyAscii < 91) Or (KeyAscii > 96 And KeyAscii < 123) Then
       If txtClientState.Text = clipboard.GetText() Then
          txtClientState.Text = ""
       End If
       If Len(txtClientState.Text) > 0 Then
          tempchar$ = Left$(txtClientState.Text, 1) & Chr$(KeyAscii)
          tempchar$ = UCase(tempchar$)
          RetCd% = State_Verify(tempchar$)
          If RetCd% <> -1 Then
             Select Case RetCd%
                Case 20, 29, 31, 37
                   txtClientState.Text = tempchar$
                   msgtext$ = "Life Savings 2 is not available in the state of " & tempchar$
                   MsgBox msgtext$
                Case Else
                   msgtext$ = ""
             End Select
             If msgtext$ = "" Then
                cboClientPlan.SetFocus
             Else
                KeyAscii = 0
                txtClientState.Text = ""
                txtClientState.SetFocus
             End If
          Else
             KeyAscii = 0
             txtClientState.Text = ""
             txtClientState.SetFocus
          End If
       End If
    End If

End Sub

Sub txtClientState_LostFocus ()

    txtClientState.ForeColor = &H80000008
    txtClientState.BackColor = &H80000005
    txtClientState.Text = UCase(txtClientState.Text)
    tempchar$ = txtClientState.Text
    If Len(tempchar$) > 0 Then
       RetCd% = State_Verify(tempchar$)
       If RetCd% = -1 Then
          txtClientState.Text = ""
          txtClientState.SetFocus
       End If
    End If

End Sub

Sub txtClientTermRider_GotFocus ()
    
    clipboard.Clear
    clipboard.SetText Trim$(txtClientTermRider.Text)
    txtClientTermRider.Text = "_______"
    txtClientTermRider.Text = clipboard.GetText()
    txtClientTermRider.ForeColor = &H80000005
    txtClientTermRider.BackColor = &H80000008

End Sub

Sub txtClientTermRider_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then txtClientFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdHelp.SetFocus
    If KeyCode = KEY_ESCAPE Then txtClientTermRider.Text = clipboard.GetText()
    If KeyCode = KEY_RETURN Or KeyCode = KEY_ESCAPE Then
       If Val(txtClientTermRider.Text) = 0 Then
          If optPremiumChoice(0).Value = True Then
             optPremiumChoice(0).SetFocus
          ElseIf optPremiumChoice(1).Value = True Then
             optPremiumChoice(1).SetFocus
          Else
             optPremiumChoice(2).SetFocus
          End If
       Else
          txtClientRiderYearEnd.Enabled = True
          cboClientRiderYrAgeEnd.Enabled = True
          txtClientRiderYearEnd.SetFocus
       End If
    End If

End Sub

Sub txtClientTermRider_LostFocus ()

    txtClientTermRider.ForeColor = &H80000008
    txtClientTermRider.BackColor = &H80000005
    
    If Val(txtClientTermRider.Text) = 0 Then
       txtClientRiderYearEnd.Text = "__"
       txtClientRiderYearEnd.Enabled = False
       cboClientRiderYrAgeEnd.Enabled = False
       txtClientTermRider.Text = "_______"
    Else
       txtClientRiderYearEnd.Enabled = True
       cboClientRiderYrAgeEnd.Enabled = True
    End If

    If Val(txtClientTermRider.Text) > 0 Then
       msgline$ = ""
       If Val(txtClientTermRider.Text) < 15000 Or Val(txtClientTermRider.Text) > 5000000 Then
          msgline$ = "Face Amount must be between $15,000 and $5,000,000, inclusive"
       End If
       If Val(txtClientTermRider.Text) < 20000 And Val(txtClientAge.Text) < 61 Then
          msgline$ = "Face Amount must be at least $20,000 for a Client age " & Format$(Val(txtClientAge.Text), "#0")
       End If
       If Val(txtClientTermRider.Text) < 25000 And Val(txtClientAge.Text) < 51 Then
          msgline$ = "Face Amount must be at least $25,000 for a Client age " & Format$(Val(txtClientAge.Text), "#0")
       End If
       If msgline$ <> "" Then
          MsgBox msgline$
          txtClientTermRider.Text = "_______"
          txtClientTermRider.SetFocus
          Exit Sub
       End If
    End If

End Sub

Sub txtPhoneContact_GotFocus ()
    
    clipboard.Clear
    clipboard.SetText Trim$(txtPhoneContact.Text)
    txtPhoneContact.Text = "(___) ___-____ Ext(_____)"
    txtPhoneContact.Text = clipboard.GetText()
    txtPhoneContact.ForeColor = &H80000005
    txtPhoneContact.BackColor = &H80000008

End Sub

Sub txtPhoneContact_KeyDown (KeyCode As Integer, Shift As Integer)
     
    If KeyCode = KEY_HOME And Shift = 4 Then txtClientFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdHelp.SetFocus
    If KeyCode = KEY_DOWN Then cmdClientList.SetFocus
    If KeyCode = KEY_UP Then txtPreparedBy.SetFocus
    If KeyCode = KEY_RETURN Then cmdClientList.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtPhoneContact = clipboard.GetText()
       cmdClientList.SetFocus
    End If

End Sub

Sub txtPhoneContact_LostFocus ()
    
    txtPhoneContact.ForeColor = &H80000008
    txtPhoneContact.BackColor = &H80000005
    AgtInfo.OfficePhone = txtPhoneContact.Text

End Sub

Sub txtPreparedBy_GotFocus ()
    
    clipboard.Clear
    clipboard.SetText Trim$(txtPreparedBy.Text)
    txtPreparedBy.Text = ""
    txtPreparedBy.Text = clipboard.GetText()
    txtPreparedBy.ForeColor = &H80000005
    txtPreparedBy.BackColor = &H80000008

End Sub

Sub txtPreparedBy_KeyDown (KeyCode As Integer, Shift As Integer)
     
    If KeyCode = KEY_HOME And Shift = 4 Then txtClientFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdHelp.SetFocus
    If KeyCode = KEY_DOWN Then txtPhoneContact.SetFocus
    If KeyCode = KEY_UP Then chkSpouseClient.SetFocus
    If KeyCode = KEY_RETURN Then txtPhoneContact.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtPreparedBy.Text = clipboard.GetText()
       txtPhoneContact.SetFocus
    End If

End Sub

Sub txtPreparedBy_LostFocus ()
    
    txtPreparedBy.ForeColor = &H80000008
    txtPreparedBy.BackColor = &H80000005
    AgtInfo.FullName = txtPreparedBy.Text
                                         
End Sub

