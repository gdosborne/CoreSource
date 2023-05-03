VERSION 2.00
Begin Form frmLIFESV 
   Caption         =   "LifeSavings Principal Insured Illustration Parameters "
   ClientHeight    =   6915
   ClientLeft      =   60
   ClientTop       =   420
   ClientWidth     =   9585
   Height          =   7320
   Icon            =   LIFESAVE.FRX:0000
   Left            =   0
   LinkTopic       =   "Form1"
   ScaleHeight     =   6915
   ScaleMode       =   0  'User
   ScaleWidth      =   9585
   Top             =   75
   Width           =   9705
   Begin Frame fraPrepAddr 
      Height          =   720
      Left            =   5400
      TabIndex        =   59
      Top             =   5430
      Width           =   3975
      Begin TextBox txtPrepAddr 
         BorderStyle     =   0  'None
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   240
         Index           =   3
         Left            =   3120
         MaxLength       =   10
         TabIndex        =   29
         Top             =   460
         Width           =   735
      End
      Begin TextBox txtPrepAddr 
         BorderStyle     =   0  'None
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   240
         Index           =   2
         Left            =   1320
         MaxLength       =   30
         TabIndex        =   28
         Top             =   460
         Width           =   1815
      End
      Begin TextBox txtPrepAddr 
         BorderStyle     =   0  'None
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   240
         Index           =   1
         Left            =   1320
         MaxLength       =   30
         TabIndex        =   27
         Top             =   290
         Width           =   2535
      End
      Begin TextBox txtPrepAddr 
         BorderStyle     =   0  'None
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   240
         Index           =   0
         Left            =   1320
         MaxLength       =   30
         TabIndex        =   26
         Top             =   120
         Width           =   2535
      End
      Begin Label lblPrepAddr 
         Caption         =   "City/State/Zip:"
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   160
         Index           =   2
         Left            =   120
         TabIndex        =   62
         Top             =   460
         Width           =   1095
      End
      Begin Label lblPrepAddr 
         Caption         =   "Address 2:"
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   160
         Index           =   1
         Left            =   120
         TabIndex        =   61
         Top             =   290
         Width           =   1095
      End
      Begin Label lblPrepAddr 
         Caption         =   "Address 1:"
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   160
         Index           =   0
         Left            =   120
         TabIndex        =   60
         Top             =   120
         Width           =   1095
      End
   End
   Begin MaskEdBox txtClientTaxID 
      Enabled         =   0   'False
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   8.25
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   285
      Left            =   7200
      Mask            =   "###-##-####"
      MaxLength       =   11
      PromptChar      =   "_"
      TabIndex        =   58
      Top             =   1200
      Visible         =   0   'False
      Width           =   1335
   End
   Begin TextBox txtClientCurrentInt 
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   8.25
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   285
      Left            =   3960
      MaxLength       =   6
      TabIndex        =   8
      Text            =   " "
      Top             =   1200
      Width           =   855
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
         TabIndex        =   23
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
         TabIndex        =   22
         Top             =   360
         Width           =   4335
      End
      Begin SSCommand cmdRiders 
         BevelWidth      =   4
         Caption         =   "INSURED &RIDERS"
         Height          =   495
         Left            =   6960
         TabIndex        =   24
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
         TabIndex        =   21
         Top             =   840
         Width           =   1935
      End
      Begin SSCommand cmdIllustrate 
         BevelWidth      =   4
         Caption         =   "&ILLUSTRATE"
         Height          =   495
         Left            =   2520
         TabIndex        =   19
         Top             =   840
         Width           =   1815
      End
      Begin SSOption optPremiumChoice 
         Caption         =   "Target Premium"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   240
         Index           =   1
         Left            =   120
         TabIndex        =   17
         TabStop         =   0   'False
         Top             =   720
         Width           =   2055
      End
      Begin SSOption optPremiumChoice 
         Caption         =   "Minimum Premium"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   240
         Index           =   2
         Left            =   120
         TabIndex        =   18
         TabStop         =   0   'False
         Top             =   1080
         Width           =   2055
      End
      Begin SSOption optPremiumChoice 
         Caption         =   "Guideline Premium"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   0
         Left            =   120
         TabIndex        =   16
         Top             =   360
         Value           =   -1  'True
         Width           =   2055
      End
      Begin SSCommand cmdPremium 
         BevelWidth      =   4
         Caption         =   "&PREMIUM CALC"
         Height          =   495
         Left            =   4800
         TabIndex        =   20
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
         Width           =   1935
      End
      Begin Label lblIll2 
         Alignment       =   2  'Center
         Caption         =   "Display/Change Planned Premium, Mode, Lump Sum Payments or Target Cash Value Before Illustrating"
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
         Caption         =   """Quick"" Illustration using Current Options (above) and Annual Premium (selected at Left)"
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
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   8.25
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   285
      Left            =   9000
      TabIndex        =   9
      Top             =   1200
      Width           =   375
   End
   Begin Frame frmPrinOptions 
      Caption         =   "Additional Riders and Substandard Rating for Client"
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   1335
      Left            =   120
      TabIndex        =   44
      Top             =   1560
      Width           =   9255
      Begin TextBox txtClientFlatRate 
         Enabled         =   0   'False
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   288
         Left            =   6120
         TabIndex        =   13
         Text            =   " "
         Top             =   960
         Width           =   972
      End
      Begin ComboBox cboClientRiderYrAgeEnd 
         Enabled         =   0   'False
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Left            =   8040
         Style           =   2  'Dropdown List
         TabIndex        =   15
         Top             =   960
         Width           =   855
      End
      Begin ComboBox cboClientWPD 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   300
         Left            =   480
         Style           =   2  'Dropdown List
         TabIndex        =   10
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
         Left            =   2520
         Style           =   2  'Dropdown List
         TabIndex        =   11
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
         Left            =   4560
         Style           =   2  'Dropdown List
         TabIndex        =   12
         Top             =   960
         Width           =   975
      End
      Begin MaskEdBox txtClientRiderYearEnd 
         Enabled         =   0   'False
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Left            =   7680
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   "_"
         TabIndex        =   14
         Top             =   960
         Width           =   375
      End
      Begin Label lblPrinSubStd 
         Alignment       =   2  'Center
         Caption         =   "Flat Extra Premium per $1000 of Death Benefit"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   372
         Index           =   1
         Left            =   5880
         TabIndex        =   57
         Top             =   600
         Width           =   1572
      End
      Begin Label lblRider 
         Alignment       =   2  'Center
         Caption         =   "Years or To Age for Flat Extra Premium"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Index           =   2
         Left            =   7560
         TabIndex        =   56
         Top             =   600
         Width           =   1455
      End
      Begin Label lblRider 
         Alignment       =   2  'Center
         Caption         =   "-------- Substandard Table Ratings and Flat Extra Premium --------"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   252
         Index           =   0
         Left            =   4560
         TabIndex        =   48
         Top             =   360
         Width           =   4332
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
         Height          =   492
         Left            =   360
         TabIndex        =   47
         Top             =   360
         Width           =   1212
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
         Height          =   492
         Left            =   2520
         TabIndex        =   46
         Top             =   360
         Width           =   1092
      End
      Begin Label lblPrinSubStd 
         Alignment       =   2  'Center
         Caption         =   "Substandard Tables"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   372
         Index           =   0
         Left            =   4560
         TabIndex        =   45
         Top             =   600
         Width           =   972
      End
   End
   Begin TextBox txtPreparedBy 
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   8.25
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   285
      Left            =   1560
      TabIndex        =   25
      Top             =   5520
      Width           =   3615
   End
   Begin ComboBox cboClientPlan 
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   8.25
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   300
      Left            =   3120
      Style           =   2  'Dropdown List
      TabIndex        =   6
      Top             =   840
      Width           =   1695
   End
   Begin TextBox txtClientSex 
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   8.25
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   285
      Left            =   9000
      TabIndex        =   5
      Top             =   480
      Width           =   375
   End
   Begin TextBox txtClientAge 
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   8.25
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   285
      Left            =   1440
      MaxLength       =   2
      TabIndex        =   3
      Text            =   " "
      Top             =   480
      Width           =   375
   End
   Begin TextBox txtClientLastName 
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   8.25
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   285
      Left            =   5280
      MaxLength       =   20
      TabIndex        =   2
      Text            =   " "
      Top             =   120
      Width           =   4095
   End
   Begin TextBox txtClientMidInit 
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   8.25
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   285
      Left            =   4920
      MaxLength       =   1
      TabIndex        =   1
      Text            =   " "
      Top             =   120
      Width           =   255
   End
   Begin TextBox txtClientFirstName 
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   8.25
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   285
      Left            =   1440
      MaxLength       =   20
      TabIndex        =   0
      Top             =   120
      Width           =   3375
   End
   Begin MaskEdBox txtPhoneContact 
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   8.25
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   300
      Left            =   2880
      Mask            =   "(###) ###-#### Ext(#####)"
      MaxLength       =   25
      PromptChar      =   "_"
      TabIndex        =   30
      Top             =   5840
      Width           =   2295
   End
   Begin MaskEdBox txtClientFace 
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   8.25
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   285
      Left            =   8520
      Mask            =   "#######"
      MaxLength       =   7
      PromptChar      =   "_"
      TabIndex        =   7
      Top             =   840
      Width           =   855
   End
   Begin MaskEdBox txtClientBirth 
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   8.25
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   285
      Left            =   3600
      Mask            =   "##/##/####"
      MaxLength       =   10
      PromptChar      =   "_"
      TabIndex        =   4
      Top             =   480
      Width           =   1215
   End
   Begin SSCommand cmdHelp 
      BevelWidth      =   4
      Caption         =   "&HELP"
      Height          =   615
      Left            =   8160
      TabIndex        =   35
      Top             =   6240
      Width           =   1215
   End
   Begin SSCommand cmdExit 
      BevelWidth      =   4
      Caption         =   "E&XIT"
      Height          =   615
      Left            =   6600
      TabIndex        =   34
      Top             =   6240
      Width           =   1215
   End
   Begin SSCommand cmdClear 
      BevelWidth      =   4
      Caption         =   "&CLEAR"
      Height          =   615
      Left            =   4680
      TabIndex        =   33
      Top             =   6240
      Width           =   1575
   End
   Begin SSCommand cmdProduct 
      BevelWidth      =   4
      Caption         =   "PRO&DUCTS"
      Height          =   615
      Left            =   2640
      TabIndex        =   32
      Top             =   6240
      Width           =   1695
   End
   Begin SSCommand cmdClientList 
      BevelWidth      =   4
      Caption         =   "CLIE&NT LIST"
      Height          =   615
      Left            =   240
      TabIndex        =   31
      Top             =   6240
      Width           =   2055
   End
   Begin Label lblCurrentInt 
      Caption         =   "Current Interest Rate:"
      Height          =   255
      Left            =   120
      TabIndex        =   43
      Top             =   1200
      Width           =   1935
   End
   Begin Label lblState 
      Caption         =   "State of Residence:"
      Height          =   252
      Left            =   5280
      TabIndex        =   40
      Top             =   1200
      Width           =   1812
   End
   Begin Label lblContactPhone 
      Caption         =   "Contact Telephone Number:"
      Height          =   255
      Left            =   240
      TabIndex        =   55
      Top             =   5840
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
      TabIndex        =   42
      Top             =   840
      Width           =   2775
   End
   Begin Label lblPlanCode 
      Caption         =   "Tobacco Use:"
      Height          =   252
      Left            =   120
      TabIndex        =   41
      Top             =   840
      Width           =   1332
   End
   Begin Label lblClientSex 
      Caption         =   "Gender:"
      Height          =   255
      Left            =   5280
      TabIndex        =   39
      Top             =   480
      Width           =   735
   End
   Begin Label lblClientBirth 
      Caption         =   "or Birthdate:"
      Height          =   255
      Left            =   2040
      TabIndex        =   38
      Top             =   480
      Width           =   1095
   End
   Begin Label lblClientAge 
      Caption         =   "Age:"
      Height          =   255
      Left            =   120
      TabIndex        =   37
      Top             =   480
      Width           =   1095
   End
   Begin Label lblClientName 
      Caption         =   "Client Name:"
      Height          =   255
      Left            =   120
      TabIndex        =   36
      Top             =   120
      Width           =   1095
   End
End

Private Sub cboClientGPO_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then cboClientSubStd.SetFocus

End Sub

Private Sub cboClientGPO_LostFocus ()
    
    If Val(txtClientAge.Text) > 35 Then
       If cboClientGPO.ListIndex <> 0 Then
          MsgText = "Guaranteed Added Death Benefit Rider not allowed; client must be less than age 36 to add."
          MsgBox MsgText, , "LifeSavings"
          cboClientGPO.ListIndex = 0
          Exit Sub
       End If
    End If
    
    If cboClientSubStd.ListIndex > 0 Then
       If cboClientGPO.ListIndex <> 0 Then
          MsgText = "Guaranteed Added Death Benefit Rider not allowed if Client has substandard table rating."
          MsgBox MsgText, , "LifeSavings"
          cboClientGPO.ListIndex = 0
          Exit Sub
       End If
    End If

End Sub

Private Sub cboClientPlan_Change ()

    ClientChanged = True

End Sub

Private Sub cboClientPlan_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then txtClientFace.SetFocus

End Sub

Private Sub cboClientRiderYrAgeEnd_KeyDown (KeyCode As Integer, Shift As Integer)

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

Private Sub cboClientRiderYrAgeEnd_LostFocus ()

    Dim TempAge As Integer
   
    If Val(txtClientFlatRate.Text) = 0 Then Exit Sub
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
             MsgText = "Age for Flat Extra Premium must be greater than Client Issue Age"
             MsgBox MsgText, , "LifeSavings"
             txtClientRiderYearEnd.SetFocus
             Exit Sub
          End If
       Case Else
          If Val(txtClientRiderYearEnd.Text) > 95 - TempAge Then
             MsgText = "Years for Flat Extra Premium cannot be greater than Client Age 95"
             MsgBox MsgText, , "LifeSavings"
             txtClientRiderYearEnd.SetFocus
             Exit Sub
          End If
    End Select
    
End Sub

Private Sub cboClientSubStd_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then cmdPremium.SetFocus

End Sub

Private Sub cboClientSubStd_LostFocus ()
    
    If cboClientSubStd.ListIndex > 0 Then
       If cboClientWPD.ListIndex <> 0 Or cboClientGPO.ListIndex <> 0 Then
          MsgText = "Disability Waiver of Cost of Insurance Rider or Guaranteed Added Death Benefit Rider not allowed on substandard illustration."
          MsgBox MsgText, , "LifeSavings"
       End If
       cboClientWPD.ListIndex = 0    'not allowed if substd
       cboClientGPO.ListIndex = 0
       'cboClientCOLA.ListIndex = 0
    End If

End Sub

Private Sub cboClientWPD_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then cboClientGPO.SetFocus

End Sub

Private Sub cboClientWPD_LostFocus ()
    
    If Val(txtClientAge.Text) > 55 Then
       If cboClientWPD.ListIndex <> 0 Then
          MsgText = "Disability Waiver of Cost of Insurance Rider not allowed; client must be less than age 56."
          MsgBox MsgText, , "LifeSavings"
          cboClientWPD.ListIndex = 0
          Exit Sub
       End If
    End If

    If Val(txtClientAge.Text) < 15 Then
       If cboClientWPD.ListIndex <> 0 Then
          MsgText = "Disability Waiver of Cost of Insurance Rider not allowed; client must be greater than age 14."
          MsgBox MsgText, , "LifeSavings"
          cboClientWPD.ListIndex = 0
          Exit Sub
       End If
    End If

    If cboClientSubStd.ListIndex > 0 Then
       If cboClientWPD.ListIndex <> 0 Then
          MsgText = "Disability Waiver of Cost of Insurance Rider not allowed if Client has substandard table rating."
          MsgBox MsgText, , "LifeSavings"
          cboClientWPD.ListIndex = 0
          Exit Sub
       End If
    End If

End Sub

Private Sub chkSpouseClient_Click (Value As Integer)

    If chkSpouseClient.Value = True Then
       cmdSpouse.Enabled = True
       cmdSpouse.Visible = True
    Else
       cmdSpouse.Enabled = False
       cmdSpouse.Visible = False
    End If

End Sub

Private Sub cmdClear_Click ()

    Dim s As Integer
    Dim t As Integer

    If ClientChanged = True And AgtInfo.Record <> 0 Then
       Call ClientSave
       If ClientSave_ReturnCode = 3 Then Exit Sub
    End If
    frmLIFESV.txtClientFirstName.Text = ""
    frmLIFESV.txtClientMidInit.Text = ""
    frmLIFESV.txtClientLastName.Text = ""
    frmLIFESV.txtClientState.Text = ""
    frmLIFESV.txtClientTaxID.Text = "___-__-____"
    frmLIFESV.txtClientSex.Text = ""
    frmLIFESV.cboClientPlan.ListIndex = 0
    frmLIFESV.txtClientAge.Text = "  "
    frmLIFESV.txtClientBirth.Text = "__/__/____"
    frmLIFESV.txtClientFace.Text = "_______"
    frmLIFESV.txtClientCurrentInt.Text = "      "
    frmLIFESV.cboClientSubStd.ListIndex = 0
    frmLIFESV.cboClientWPD.ListIndex = 0
    frmLIFESV.cboClientGPO.ListIndex = 0
    'frmLIFESV.cboClientCOLA.ListIndex = 0
    frmLIFESV.txtClientRiderYearEnd.Text = "__"
    frmLIFESV.cboClientRiderYrAgeEnd.ListIndex = 0
    
    For s = 0 To 1
      LSClient(s).Mode = " "
      LSClient(s).PlannedPrem = 0
      LSClient(s).YearsToPay = 0
      LSClient(s).LumpSum = 0
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
      LSFutureWD(s).WD_Age = 0
      LSFutureWD(s).Loan_Age = 0
      LSFutureWD(s).LoanPay_Age = 0
    Next s
    AgeChanged = 0
    
End Sub

Private Sub cmdClientList_Click ()
    
    On Error GoTo ErrClientList
    
    If ClientChanged = True And AgtInfo.Record <> 0 Then
       Call ClientSave
       If ClientSave_ReturnCode = 3 Then Exit Sub
    End If
    
    No_Clients = False
    
    If UCase$(Left$(ClientFile, 4)) <> "CLNT" Then
       MsgText = "No Clients on file"
       MsgBox MsgText, 64, "LifeSavings"
       No_Clients = True
       Exit Sub
    End If
    
    Call Refresh_ClntList
    Unload frmLIFESV
    frmClntList.Show
    Exit Sub

ErrClientList:
    Select Case Err
        Case 364
            Resume Next
        Case Else
            MsgText = "Err: " & Error$(Err) & "," & Str$(Err) & "Client List"
            MsgBox MsgText, 64, "LifeSavings"
            Exit Sub
    End Select

End Sub

Private Sub cmdExit_Click ()

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

Private Sub cmdFuture_Click ()
    
    Call Set_Client_Illus_Parameters
    frmLIFESV.Hide
    frmLSCHANGE.Show 0

End Sub

Private Sub cmdHelp_Click ()
    
    Dim i As Integer
    
    i = WinHelp(frmLIFESV.hWnd, "ls3\lifesav3.hlp" + Chr$(0), 1&, 1&)

End Sub

Private Sub cmdIllustrate_Click ()

    s = SpouseClient
    Call Set_Client_Illus_Parameters

    i = ReadyToIllustrate()
    If i <> 0 Then
       Select Case (i)
          Case 1
             MsgText = "Cannot illustrate until client issue age is valid."
          Case 2
             MsgText = "Cannot illustrate until client gender is valid."
          Case 3
             MsgText = "Cannot illustrate until client face amount is valid."
          Case 4
             MsgText = "Cannot illustrate until client face amount is valid for state."
          Case 5
             MsgText = "Cannot illustrate until client interest rate is valid."
          Case Else
       End Select
       MsgBox MsgText, , "LifeSavings"
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

Private Sub cmdPremium_Click ()

    Call Set_Client_Illus_Parameters
    frmLIFESV.Hide
    frmLSPREM.Show 0

End Sub

Private Sub cmdProduct_Click ()

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

Private Sub cmdRiders_Click ()

    Call Set_Client_Illus_Parameters
    frmLIFESV.Hide
    frmLSINSD.Show 0

End Sub

Private Sub cmdSpouse_Click ()

    Call Set_Client_Illus_Parameters
    frmLIFESV.Hide
    frmLSSPOUSE.Show 0

End Sub

Sub Form_DblClick ()

    Dim ExeName As String
    Dim ExeDateTime As Variant
    
    ExeName = CurDir$ & "\Lifesav3.exe"

    MessageText = "Copyright " & Chr$(169) & "1998 American Republic Insurance Company" & Chr$(13)
    MessageText = MessageText & "Version: Life Savings 3.10" & Chr$(13)
    MessageText = MessageText & "Verified Year 2000 compliant on 06/25/1998" & Chr$(13)

    On Error Resume Next
    ExeDateTime = FileDateTime(ExeName)
    If Err Then    'check two places for executable
       Err = 0
       ExeName = CurDir$ & "\ls3\Lifesav3.exe"
       ExeDateTime = FileDateTime(ExeName)
    End If
    If Err Then
       MessageText = MessageText & "Unable to verify EXE date and time." & Chr$(13)
    Else
       MessageText = MessageText & "Date: " & Format$(ExeDateTime, "mmmm d,yyyy") & Chr$(13)
       MessageText = MessageText & "Time: " & Format$(ExeDateTime, "h:mm AM/PM")
    End If
    MsgBox MessageText, , "Life Savings"

End Sub

Private Sub Form_Load ()

    Dim RetStr As String

    If CallFromMain <> "NO" Then
       CommandString = Command$
       Call Parse_Command_Argument(CommandString, AgtInfo.Record, ClientInfo.ClientNumber)
    End If

    RetStr = GetAnyIniStr("Americare", "LS3Face", "AMRICARE.INI")
    If Trim$(RetStr) <> "" Then
       If Val(RetStr) = 99 Then
          defFaceLimit = 1
       Else
          defFaceLimit = 0
       End If
    Else
       defFaceLimit = 0
    End If
    
    RetStr = GetAnyIniStr("Americare", "LS3Zoom", "AMRICARE.INI")
    If Trim$(RetStr) <> "" Then
       defZoomSize = Val(RetStr)
       If defZoomSize > 175 Then defZoomSize = 175
    Else
       defZoomSize = 75
    End If
    
    RetStr = GetAnyIniStr("Americare", "PrintCert", "AMRICARE.INI")
    If Trim$(RetStr) <> "" Then
       If Val(RetStr) = 99 Then
          defPrintCertif = 1
       Else
          defPrintCertif = 0
       End If
    Else
       defPrintCertif = 0
    End If
    
    RetStr = GetAnyIniStr("Americare", "ShowMono", "AMRICARE.INI")
    If Val(RetStr) = 0 Then
       defShowMono = 0    'Monochrome preview in vsPrint
    Else
       defShowMono = 1    'Screen compatible preview
    End If
    
    Call Open_LS_RateFiles
    
    LSClient(0).ClientName = ""
    LSClient(1).ClientName = ""

    SpouseClient = 0
    cboClientPlan.AddItem "No Tobacco Use"
    cboClientPlan.AddItem "Tobacco User"
    cboClientPlan.ListIndex = 0
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
    'cboClientCOLA.AddItem "None"
    'cboClientCOLA.AddItem "1%"
    'cboClientCOLA.AddItem "2%"
    'cboClientCOLA.AddItem "3%"
    'cboClientCOLA.AddItem "4%"
    'cboClientCOLA.AddItem "5%"
    'cboClientCOLA.AddItem "6%"
    'cboClientCOLA.AddItem "7%"
    'cboClientCOLA.AddItem "8%"
    'cboClientCOLA.AddItem "9%"
    'cboClientCOLA.AddItem "10%"
    'cboClientCOLA.ListIndex = 0
    cboClientWPD.AddItem "No"
    cboClientWPD.AddItem "Yes"
    cboClientWPD.ListIndex = 0
    
    Call Load_LS_From_Client
    AgeChanged = 0

End Sub

Private Sub Form_Unload (Cancel As Integer)

    Dim i As Integer

    CallFromMain = "NO"
    Call Close_LS_RateFiles
    i = WinHelp(frmLIFESV.hWnd, dummy$, 2&, 0&)
    If AgtInfo.Record = 0 Then Call Save_LifePro

    Set frmLIFESV = Nothing
   
End Sub

Sub lblClientAge_DblClick ()

    AgeChanged = 3

End Sub

Private Sub txtClientAge_Change ()
 
    ClientChanged = True
    If AgeChanged <> 3 Then AgeChanged = 1

End Sub

Private Sub txtClientAge_GotFocus ()
 
    Clipboard.Clear
    Clipboard.SetText Trim$(txtClientAge.Text)
    txtClientAge.Text = ""
    txtClientAge.Text = Clipboard.GetText()
    txtClientAge.ForeColor = &H80000005
    txtClientAge.BackColor = &H80000008
    If AgeChanged <> 3 Then AgeChanged = 0

End Sub

Private Sub txtClientAge_KeyDown (KeyCode As Integer, Shift As Integer)
    
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
             txtClientAge.Text = Clipboard.GetText()
             If AgeChanged <> 3 Then AgeChanged = 2
          End If
          Call txtClientAge_LostFocus
          If KeyCode = KEY_DOWN Then cboClientPlan.SetFocus
          If KeyCode = KEY_UP Then txtClientFirstName.SetFocus
    End Select

End Sub

Private Sub txtClientAge_KeyPress (KeyAscii As Integer)

    If KeyAscii < 48 Or KeyAscii > 57 Then
       KeyAscii = 0
    Else
       If txtClientAge.Text = Clipboard.GetText() Then
          txtClientAge.Text = ""
       End If
    End If

End Sub

Private Sub txtClientAge_KeyUp (KeyCode As Integer, Shift As Integer)

    If (KeyCode > 47 And KeyCode < 58) Or (KeyCode > 95 And KeyCode < 106) Then
       If Len(txtClientAge.Text) >= 2 Then
          If AgeChanged <> 3 Then AgeChanged = 2
          Call txtClientAge_LostFocus
       End If
    End If

End Sub

Private Sub txtClientAge_LostFocus ()

    Dim TempAge As Integer

    If AgeChanged = 0 Or txtClientAge.Text = "" Then GoTo End_AgeProc
    TempAge = Val(txtClientAge.Text)
    MsgText = ""
    If TempAge > 80 Then
       MsgText = "Issue age must be less than 81 for a LifeSavings client"
    End If
    If MsgText <> "" Then
       MsgBox MsgText, , "LifeSavings"
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
   'If TempAge > 60 Then cboClientCOLA.ListIndex = 0

End_AgeProc:
    
    If AgeChanged >= 2 Then txtClientBirth.SetFocus
    If Val(txtClientFace.Text) > 0 And defFaceLimit = 0 Then
       Select Case TempAge
          Case 61 To 70
             If Val(txtClientFace.Text) > 0 And Val(txtClientFace.Text) < 15000 Then
                txtClientFace.Text = "15000__"
             End If
          Case 51 To 60
             If Val(txtClientFace.Text) > 0 And Val(txtClientFace.Text) < 20000 Then
                txtClientFace.Text = "20000__"
             End If
          Case Else
             If Val(txtClientFace.Text) > 0 And Val(txtClientFace.Text) < 25000 Then
                txtClientFace.Text = "25000__"
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

Private Sub txtClientBirth_Change ()
 
    ClientChanged = True
    BirthChanged = 1

End Sub

Private Sub txtClientBirth_GotFocus ()
 
    Clipboard.Clear
    Clipboard.SetText Trim$(txtClientBirth.Text)
    txtClientBirth.Text = "__/__/____"
    txtClientBirth.Text = Clipboard.GetText()
    txtClientBirth.ForeColor = &H80000005
    txtClientBirth.BackColor = &H80000008
    BirthChanged = 0

End Sub

Private Sub txtClientBirth_KeyDown (KeyCode As Integer, Shift As Integer)

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
             txtClientBirth.Text = Clipboard.GetText()
             BirthChanged = 2
          End If
          Call txtClientBirth_LostFocus
          If KeyCode = KEY_DOWN Then cboClientPlan.SetFocus
          If KeyCode = KEY_UP Then txtClientFirstName.SetFocus
    End Select

End Sub

Private Sub txtClientBirth_KeyUp (KeyCode As Integer, Shift As Integer)
       
    If (KeyCode > 47 And KeyCode < 58) Or (KeyCode > 95 And KeyCode < 106) Then
       If Mid$(txtClientBirth.Text, 10, 1) <> "_" And txtClientBirth.Text <> Clipboard.GetText() Then
          BirthChanged = 2
          Call txtClientBirth_LostFocus
       End If
    End If

End Sub

Private Sub txtClientBirth_LostFocus ()

    If BirthChanged = 0 Or txtClientBirth.Text = "__/__/____" Then GoTo End_BirthProc
    tempbirth$ = txtClientBirth.Text
    Call Fetch_Life_Birth(RetCode, tempbirth$)
    If RetCode = False Then
       txtClientBirth.Text = "__/__/____"
       MsgText = "Invalid birthdate"
       MsgBox MsgText, , "LifeSavings"
       txtClientBirth.SetFocus
       Exit Sub
    End If
    If AgeChanged = 3 Then GoTo End_BirthProc
    If Left$(txtClientBirth.Text, 2) <> "__" Then
       mm$ = Left$(txtClientBirth.Text, 2)
       dd$ = Mid$(txtClientBirth.Text, 4, 2)
       yy$ = Right$(txtClientBirth.Text, 4)
       If Left$(txtClientAge.Text, 2) <> "  " Then
          Xyy$ = Str$(Year(Now) - Val(txtClientAge.Text))
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
       If cage% > 80 Then
          MsgText = "Birthdate must be such that issue age is less than 81 for a LifeSavings client"
       End If
       If MsgText <> "" Then
          MsgBox MsgText, , "LifeSavings"
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

Private Sub txtClientCurrentInt_GotFocus ()
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtClientCurrentInt.Text)
    txtClientCurrentInt.Text = ""
    txtClientCurrentInt.Text = Clipboard.GetText()
    txtClientCurrentInt.ForeColor = &H80000005
    txtClientCurrentInt.BackColor = &H80000008

End Sub

Private Sub txtClientCurrentInt_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then txtClientFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdHelp.SetFocus
    If KeyCode = KEY_RETURN Then txtClientState.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtClientCurrentInt.Text = Clipboard.GetText()
       txtClientState.SetFocus
    End If

End Sub

Private Sub txtClientCurrentInt_KeyPress (KeyAscii As Integer)

    If (KeyAscii < 48 Or KeyAscii > 57) And KeyAscii <> 46 Then KeyAscii = 0

End Sub

Private Sub txtClientCurrentInt_LostFocus ()
    
    Dim TempInt As Single
    
    txtClientCurrentInt.ForeColor = &H80000008
    txtClientCurrentInt.BackColor = &H80000005
    If Val(txtClientCurrentInt.Text) > 0 And (Val(txtClientCurrentInt.Text) < 3# Or Val(txtClientCurrentInt.Text) > 12#) Then
       MsgText = "Current Interest Rate must be between 3.00% and the Current Interest Rate.  Do not use an Interest Rate that is greater than the Current Rate."
       MsgBox MsgText, , "LifeSavings"
       txtClientCurrentInt.Text = ""
       txtClientCurrentInt.SetFocus
       Exit Sub
    End If
    TempInt = Val(txtClientCurrentInt.Text)
    If TempInt > 0 Then txtClientCurrentInt.Text = Format$(TempInt, "00.000")

End Sub

Private Sub txtClientFace_GotFocus ()
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtClientFace.Text)
    txtClientFace.Text = "0000000"
    txtClientFace.Text = Clipboard.GetText()
    txtClientFace.ForeColor = &H80000005
    txtClientFace.BackColor = &H80000008

End Sub

Private Sub txtClientFace_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then txtClientFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdHelp.SetFocus
    If KeyCode = KEY_RETURN Then txtClientCurrentInt.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtClientFace.Text = Clipboard.GetText()
       txtClientCurrentInt.SetFocus
    End If

End Sub

Private Sub txtClientFace_LostFocus ()

    txtClientFace.ForeColor = &H80000008
    txtClientFace.BackColor = &H80000005
    If Val(txtClientFace.Text) > 0 Then
       MsgText = ""
       If defFaceLimit = 0 Then
          If Val(txtClientFace.Text) < 15000 Or Val(txtClientFace.Text) > 9999999 Then
             MsgText = "Face Amount must be between $15,000 and $9,999,999, inclusive"
          End If
       Else
          If Val(txtClientFace.Text) < 1 Or Val(txtClientFace.Text) > 9999999 Then
             MsgText = "Face Amount must be between $1 and $9,999,999, inclusive"
          End If
       End If
       If MsgText <> "" Then
          MsgBox MsgText, , "LifeSavings"
          txtClientFace.Text = "_______"
          txtClientFace.SetFocus
          Exit Sub
       End If
    End If

End Sub

Private Sub txtClientFirstName_Change ()
    
    If txtClientFirstName.Text <> Clipboard.GetText() Then
       ClientChanged = True
    Else
       ClientChanged = False
    End If

End Sub

Private Sub txtClientFirstName_GotFocus ()
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtClientFirstName.Text)
    txtClientFirstName.Text = ""
    txtClientFirstName.Text = Clipboard.GetText()
    txtClientFirstName.ForeColor = &H80000005
    txtClientFirstName.BackColor = &H80000008

End Sub

Private Sub txtClientFirstName_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_END And Shift = 4 Then cmdHelp.SetFocus
    If KeyCode = KEY_DOWN Then txtClientAge.SetFocus
    If KeyCode = KEY_ESCAPE Then txtClientFirstName.Text = Clipboard.GetText()
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

Private Sub txtClientFirstName_LostFocus ()

    txtClientFirstName.ForeColor = &H80000008
    txtClientFirstName.BackColor = &H80000005
     
End Sub

Private Sub txtClientFlatRate_GotFocus ()
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtClientFlatRate.Text)
    txtClientFlatRate.Text = ""
    txtClientFlatRate.Text = Clipboard.GetText()
    txtClientFlatRate.ForeColor = &H80000005
    txtClientFlatRate.BackColor = &H80000008

End Sub

Private Sub txtClientFlatRate_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then txtClientFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdHelp.SetFocus
    If KeyCode = KEY_RETURN Then txtClientRiderYearEnd.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtClientFlatRate.Text = Clipboard.GetText()
       txtClientRiderYearEnd.SetFocus
    End If

End Sub

Private Sub txtClientFlatRate_KeyPress (KeyAscii As Integer)

    If (KeyAscii < 48 Or KeyAscii > 57) And KeyAscii <> 46 Then KeyAscii = 0

End Sub

Private Sub txtClientFlatRate_LostFocus ()
    
    Dim TempFlat As Currency
    
    txtClientFlatRate.ForeColor = &H80000008
    txtClientFlatRate.BackColor = &H80000005
    If Val(txtClientFlatRate.Text) > 0 And (Val(txtClientFlatRate.Text) < 1# Or Val(txtClientFlatRate.Text) > 25#) Then
       MsgText = "Flat Extra Premium must be between $1.00 and $25.00, inclusive"
       MsgBox MsgText, , "LifeSavings"
       txtClientFlatRate.Text = ""
       txtClientFlatRate.SetFocus
       Exit Sub
    End If
    TempFlat = Val(txtClientFlatRate.Text)
    If TempFlat > 0 Then txtClientFlatRate.Text = "     " & Format$(TempFlat, "#0.00")

End Sub

Private Sub txtClientLastName_Change ()
 
    If txtClientLastName.Text <> Clipboard.GetText() Then
       ClientChanged = True
    Else
       ClientChanged = False
    End If

End Sub

Private Sub txtClientLastName_GotFocus ()

    Clipboard.Clear
    Clipboard.SetText Trim$(txtClientLastName.Text)
    txtClientLastName.Text = ""
    txtClientLastName.Text = Clipboard.GetText()
    txtClientLastName.ForeColor = &H80000005
    txtClientLastName.BackColor = &H80000008

End Sub

Private Sub txtClientLastName_KeyDown (KeyCode As Integer, Shift As Integer)
     
    If KeyCode = KEY_HOME And Shift = 4 Then txtClientFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdHelp.SetFocus
    If KeyCode = KEY_DOWN Then txtClientAge.SetFocus
    If KeyCode = KEY_RETURN Then txtClientAge.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtClientLastName.Text = Clipboard.GetText()
       txtClientAge.SetFocus
    End If

End Sub

Private Sub txtClientLastName_LostFocus ()
    
    txtClientLastName.ForeColor = &H80000008
    txtClientLastName.BackColor = &H80000005
    onechar$ = Left$(txtClientLastName, 1)
    onechar$ = UCase(onechar$)
    txtClientLastName.Text = onechar$ & Mid$(txtClientLastName.Text, 2)

End Sub

Private Sub txtClientMidInit_GotFocus ()
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtClientMidInit.Text)
    txtClientMidInit.Text = ""
    txtClientMidInit.Text = Clipboard.GetText()
    txtClientMidInit.ForeColor = &H80000005
    txtClientMidInit.BackColor = &H80000008

End Sub

Private Sub txtClientMidInit_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then txtClientFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdHelp.SetFocus
    If KeyCode = KEY_DOWN Then txtClientAge.SetFocus
    If KeyCode = KEY_RETURN Then txtClientLastName.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtClientMidInit.Text = Clipboard.GetText()
       txtClientLastName.SetFocus
    End If

End Sub

Private Sub txtClientMidInit_LostFocus ()
     
    txtClientMidInit.ForeColor = &H80000008
    txtClientMidInit.BackColor = &H80000005
    txtClientMidInit.Text = UCase$(txtClientMidInit.Text)

End Sub

Private Sub txtClientRiderYearEnd_GotFocus ()
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtClientRiderYearEnd.Text)
    txtClientRiderYearEnd.Text = "__"
    If Val(Clipboard.GetText()) <> 0 Then
       txtClientRiderYearEnd.Text = Clipboard.GetText()
    End If
    txtClientRiderYearEnd.ForeColor = &H80000005
    txtClientRiderYearEnd.BackColor = &H80000008

End Sub

Private Sub txtClientRiderYearEnd_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then txtClientFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdHelp.SetFocus
    If KeyCode = KEY_RETURN Then cboClientRiderYrAgeEnd.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtClientRiderYearEnd.Text = Clipboard.GetText()
       cboClientRiderYrAgeEnd.SetFocus
    End If

End Sub

Private Sub txtClientRiderYearEnd_LostFocus ()
    
    txtClientRiderYearEnd.ForeColor = &H80000008
    txtClientRiderYearEnd.BackColor = &H80000005

End Sub

Private Sub txtClientSex_Change ()
 
    ClientChanged = True

End Sub

Private Sub txtClientSex_GotFocus ()
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtClientSex.Text)
    txtClientSex.Text = ""
    txtClientSex.Text = Clipboard.GetText()
    txtClientSex.ForeColor = &H80000005
    txtClientSex.BackColor = &H80000008

End Sub

Private Sub txtClientSex_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then txtClientFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdHelp.SetFocus
    If KeyCode = KEY_UP Then txtClientFirstName.SetFocus
    If KeyCode = KEY_DOWN Then txtClientFace.SetFocus
    If KeyCode = KEY_RETURN Then cboClientPlan.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtClientSex.Text = Clipboard.GetText()
       txtClientFace.SetFocus
    End If

End Sub

Private Sub txtClientSex_KeyPress (KeyAscii As Integer)
     
    Select Case (KeyAscii)
       Case 32, 70, 77, 102, 109
          If txtClientSex.Text = Clipboard.GetText() Then
             txtClientSex.Text = ""
             cboClientPlan.SetFocus
          End If
       Case Else
          KeyAscii = 0
    End Select

End Sub

Private Sub txtClientSex_LostFocus ()
    
    txtClientSex.ForeColor = &H80000008
    txtClientSex.BackColor = &H80000005
    txtClientSex.Text = UCase$(txtClientSex.Text)

End Sub

Private Sub txtClientState_GotFocus ()
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtClientState.Text)
    txtClientState.Text = ""
    txtClientState.Text = Clipboard.GetText()
    txtClientState.ForeColor = &H80000005
    txtClientState.BackColor = &H80000008

End Sub

Private Sub txtClientState_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then txtClientFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdHelp.SetFocus
    If KeyCode = KEY_UP Then txtClientFirstName.SetFocus
    If KeyCode = KEY_DOWN Then cboClientWPD.SetFocus
    If KeyCode = KEY_RETURN Then cboClientWPD.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtClientState.Text = Clipboard.GetText()
       cboClientWPD.SetFocus
    End If

End Sub

Private Sub txtClientState_KeyPress (KeyAscii As Integer)
    
    If KeyAscii = 32 Or (KeyAscii > 64 And KeyAscii < 91) Or (KeyAscii > 96 And KeyAscii < 123) Then
       If txtClientState.Text = Clipboard.GetText() Then
          txtClientState.Text = ""
       End If
       If Len(txtClientState.Text) > 0 Then
          TempChar$ = Left$(txtClientState.Text, 1) & Chr$(KeyAscii)
          TempChar$ = UCase(TempChar$)
          RetCode = State_Verify(TempChar$)
          If RetCode <> -1 Then
             Select Case RetCode
                'Invalid States: Maryland (19), Massachusetts (20), New Jersey (29),
                'New York (29).  Pennsylvania (37) activated on 05/01/1998.
                'Michigan(21), Texas (42) activated on 7/10/2000
                Case 19, 20, 29, 31
                   txtClientState.Text = TempChar$
                   MsgText = "LifeSavings is not available in the state of " & TempChar$
                   MsgBox MsgText, , "LifeSavings"
                Case Else
                   MsgText = ""
             End Select
             If MsgText = "" Then
                cboClientWPD.SetFocus
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

Private Sub txtClientState_LostFocus ()

    Dim TempChar As String * 2

    txtClientState.ForeColor = &H80000008
    txtClientState.BackColor = &H80000005
    txtClientState.Text = UCase(txtClientState.Text)
    TempChar = txtClientState.Text
    If Len(TempChar) > 0 Then
       RetCode = State_Verify(TempChar)
       If RetCode = -1 Then
          txtClientState.Text = ""
          txtClientState.SetFocus
       End If
    End If

    If TempChar = "MI" Then
       lblState.Caption = "Client Tax ID:"
       txtClientTaxID.Enabled = True
       txtClientTaxID.Visible = True
       txtClientTaxID.SetFocus
    End If

End Sub

Sub txtClientTaxID_GotFocus ()
    
    txtClientTaxID.ForeColor = &H80000005
    txtClientTaxID.BackColor = &H80000008

End Sub

Sub txtClientTaxID_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_HOME And Shift = 4 Then txtClientFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdHelp.SetFocus
    If KeyCode = KEY_UP Then txtClientFace.SetFocus
    If KeyCode = KEY_DOWN Or KeyCode = KEY_RETURN Or KeyCode = KEY_ESCAPE Then cboClientWPD.SetFocus

End Sub

Sub txtClientTaxID_LostFocus ()
       
    If Val(txtClientTaxID.Text) = 0 Then txtClientTaxID.Text = "999-99-9999"
    lblState.Caption = "State of Residence:"
    txtClientTaxID.ForeColor = &H80000008
    txtClientTaxID.BackColor = &H80000005
    txtClientTaxID.Enabled = False
    txtClientTaxID.Visible = False
    cboClientWPD.SetFocus

End Sub

Private Sub txtPhoneContact_GotFocus ()
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtPhoneContact.Text)
    txtPhoneContact.Text = "(___) ___-____ Ext(_____)"
    txtPhoneContact.Text = Clipboard.GetText()
    txtPhoneContact.ForeColor = &H80000005
    txtPhoneContact.BackColor = &H80000008

End Sub

Private Sub txtPhoneContact_KeyDown (KeyCode As Integer, Shift As Integer)
     
    If KeyCode = KEY_HOME And Shift = 4 Then txtClientFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdHelp.SetFocus
    If KeyCode = KEY_DOWN Then cmdClientList.SetFocus
    If KeyCode = KEY_UP Then txtPreparedBy.SetFocus
    If KeyCode = KEY_RETURN Then cmdClientList.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtPhoneContact = Clipboard.GetText()
       cmdClientList.SetFocus
    End If

End Sub

Private Sub txtPhoneContact_LostFocus ()
    
    txtPhoneContact.ForeColor = &H80000008
    txtPhoneContact.BackColor = &H80000005
    AgtInfo.OfficePhone = txtPhoneContact.Text

End Sub

Sub txtPrepAddr_GotFocus (Index As Integer)
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtPrepAddr(Index).Text)
    txtPrepAddr(Index).Text = ""
    txtPrepAddr(Index).Text = Clipboard.GetText()
    txtPrepAddr(Index).ForeColor = &H80000005
    txtPrepAddr(Index).BackColor = &H80000008

End Sub

Sub txtPrepAddr_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)
     
    If KeyCode = KEY_HOME And Shift = 4 Then txtClientFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdHelp.SetFocus
    If KeyCode = KEY_UP Then chkSpouseClient.SetFocus
    If KeyCode = KEY_RETURN Or KeyCode = KEY_DOWN Then
       If Index > 2 Then
          txtPhoneContact.SetFocus
       Else
          txtPrepAddr(Index + 1).SetFocus
       End If
    End If
    If KeyCode = KEY_ESCAPE Then
       txtPrepAddr(Index).Text = Clipboard.GetText()
       If Index > 2 Then
          txtPhoneContact.SetFocus
       Else
          txtPrepAddr(Index + 1).SetFocus
       End If
    End If

End Sub

Sub txtPrepAddr_KeyPress (Index As Integer, KeyAscii As Integer)

    If Index = 3 Then
       If KeyAscii <> 45 And (KeyAscii < 48 Or KeyAscii > 57) Then KeyAscii = 0
    End If

End Sub

Sub txtPrepAddr_LostFocus (Index As Integer)
    
    txtPrepAddr(Index).ForeColor = &H80000008
    txtPrepAddr(Index).BackColor = &H80000005
    Select Case (Index)
       Case 0
          AgtInfo.Address1 = txtPrepAddr(Index).Text
       Case 1
          AgtInfo.Address2 = txtPrepAddr(Index).Text
       Case 2
          If txtPrepAddr(Index).Text <> "" Then
             If Len(Trim$(txtPrepAddr(Index).Text)) <= 2 Then
                MsgText = "Enter a valid City and two character State Code in this field.  Enter Zip Code in the next field to the right; press 'ENTER' or 'TAB' after entering a valid City and State."
                MsgBox MsgText, , "LifeSavings"
                txtPrepAddr(Index).SetFocus
             Else
                AgtInfo.City = Left$(Trim$(txtPrepAddr(Index).Text), Len(Trim$(txtPrepAddr(Index).Text)) - 2)
                AgtInfo.State = Right$(Trim$(txtPrepAddr(Index).Text), 2)
                AgtInfo.State = UCase$(AgtInfo.State)
                If Mid$(Trim$(txtPrepAddr(Index).Text), Len(Trim$(txtPrepAddr(Index).Text)) - 2, 1) <> " " Then
                   MsgText = "City and two character State Code should be entered in this field.  The Zip Code goes in the next field to the right; press 'ENTER' or 'TAB'."
                   MsgBox MsgText, , "LifeSavings"
                   txtPrepAddr(Index).SetFocus
                End If
             End If
          End If
       Case Else
          AgtInfo.ZipCode = Val(txtPrepAddr(Index).Text)
    End Select

End Sub

Private Sub txtPreparedBy_GotFocus ()
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtPreparedBy.Text)
    txtPreparedBy.Text = ""
    txtPreparedBy.Text = Clipboard.GetText()
    txtPreparedBy.ForeColor = &H80000005
    txtPreparedBy.BackColor = &H80000008

End Sub

Private Sub txtPreparedBy_KeyDown (KeyCode As Integer, Shift As Integer)
     
    If KeyCode = KEY_HOME And Shift = 4 Then txtClientFirstName.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdHelp.SetFocus
    If KeyCode = KEY_UP Then chkSpouseClient.SetFocus
    If KeyCode = KEY_RETURN Or KeyCode = KEY_DOWN Then txtPrepAddr(0).SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtPreparedBy.Text = Clipboard.GetText()
       txtPrepAddr(0).SetFocus
    End If

End Sub

Private Sub txtPreparedBy_LostFocus ()
    
    txtPreparedBy.ForeColor = &H80000008
    txtPreparedBy.BackColor = &H80000005
    AgtInfo.FullName = txtPreparedBy.Text
                                         
End Sub

