;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;
;               설치파일 
;

; HM NIS Edit Wizard helper defines
!define PRODUCT_NAME "SNet3.Monitor"
!define PRODUCT_VERSION "1.1.2"
!define PRODUCT_PUBLISHER "Waton Inc."
!define PRODUCT_WEB_SITE "http://www.waton.co.kr"
!define PRODUCT_DIR_REGKEY "Software\Microsoft\Windows\CurrentVersion\App Paths\SNet3.Monitor.exe"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"

;--------------------------------
; MUI 1.67 compatible ------
; Include Modern UI

!include "MUI.nsh"
!include "LogicLib.nsh"
!include "DotNetVer.nsh"

;--------------------------------
;Interface Settings

; MUI Settings
!define MUI_ABORTWARNING
!define MUI_ICON "${NSISDIR}\Contrib\Graphics\Icons\orange-install.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\orange-uninstall.ico"

; Language Selection Dialog Settings
!define MUI_LANGDLL_REGISTRY_ROOT "${PRODUCT_UNINST_ROOT_KEY}"
!define MUI_LANGDLL_REGISTRY_KEY "${PRODUCT_UNINST_KEY}"
!define MUI_LANGDLL_REGISTRY_VALUENAME "NSIS:Language"


;--------------------------------
;Pages

; Welcome page
!insertmacro MUI_PAGE_WELCOME
; License page
!insertmacro MUI_PAGE_LICENSE $(LicenseRTF)
; Directory page
!insertmacro MUI_PAGE_DIRECTORY
; Instfiles page
!insertmacro MUI_PAGE_INSTFILES
; Finish page
!define MUI_FINISHPAGE_RUN "$INSTDIR\SNet3.Monitor.exe"
!insertmacro MUI_PAGE_FINISH

; Uninstaller pages
!insertmacro MUI_UNPAGE_INSTFILES

;--------------------------------
; Language files

!insertmacro MUI_LANGUAGE "English"
!insertmacro MUI_LANGUAGE "Korean"

;--------------------------------
;License Language String

LicenseLangString LicenseRTF ${LANG_ENGLISH} "D:\Workspaces\OldWatonProject\Snet2_New_Maria\Products\Bqms\Deploy\Resource\eula_en.rtf"
LicenseLangString LicenseRTF ${LANG_KOREAN} "D:\Workspaces\OldWatonProject\Snet2_New_Maria\Products\Bqms\Deploy\Resource\eula_ko.rtf"


; MUI end ------

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "D:\publish\SNet3.Monitor\SNet3.Monitor v1.1.2.exe"
InstallDir "$PROGRAMFILES\Waton\SNet3.Monitor"
InstallDirRegKey HKLM "${PRODUCT_DIR_REGKEY}" ""
ShowInstDetails show
ShowUnInstDetails show

RequestExecutionLevel admin 

Function .onInit
  ${If} ${DOTNETVER_4_0} HasDotNetFullProfile 0
	ExecWait "DotNetFX40\dotNetFx40_Full_x86_x64.exe"
  ${EndIf}
  !insertmacro MUI_LANGDLL_DISPLAY
FunctionEnd

Section "MainSection" SEC01

  
  SetShellVarContext all
  SetOutPath "$INSTDIR"
  SetOverwrite on 

  File "D:\Workspaces\WatonProject\SNet3\SNet3.Monitor\bin\Release\SNet3.Core.dll"
  File "D:\Workspaces\WatonProject\SNet3\SNet3.Monitor\bin\Release\SNet3.Monitor.exe"
  File "D:\Workspaces\WatonProject\SNet3\SNet3.Monitor\bin\Release\SNet3.Monitor.exe.config"
  File "D:\Workspaces\WatonProject\SNet3\SNet3.Monitor\bin\Release\MvvmHelpers.dll"
  
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\DevExpress.Data.v18.2.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\DevExpress.Images.v18.2.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\DevExpress.Mvvm.v18.2.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\DevExpress.Pdf.v18.2.Core.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\DevExpress.Printing.v18.2.Core.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\DevExpress.Xpf.Controls.v18.2.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\DevExpress.Xpf.Core.v18.2.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\DevExpress.Xpf.Grid.v18.2.Core.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\DevExpress.Xpf.Grid.v18.2.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\DevExpress.Xpf.LayoutControl.v18.2.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\DevExpress.Xpf.Ribbon.v18.2.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\DevExpress.Xpf.Themes.Office2016White.v18.2.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\DevExpress.Xpf.Themes.Office2016WhiteSE.v18.2.dll"
  CreateDirectory "$SMPROGRAMS\Waton\SNet3.Monitor"
    
  
  SetShellVarContext all
  SetOutPath "$INSTDIR\de"
  SetOverwrite on 
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\de\DevExpress.Data.v18.2.resources.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\de\DevExpress.Pdf.v18.2.Core.resources.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\de\DevExpress.Printing.v18.2.Core.resources.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\de\DevExpress.Xpf.Controls.v18.2.resources.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\de\DevExpress.Xpf.Core.v18.2.resources.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\de\DevExpress.Xpf.Grid.v18.2.Core.resources.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\de\DevExpress.Xpf.LayoutControl.v18.2.resources.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\de\DevExpress.Xpf.Ribbon.v18.2.resources.dll"
  CreateDirectory "$SMPROGRAMS\Waton\SNet3.Monitor\de"    
  
  SetShellVarContext all
  SetOutPath "$INSTDIR\es"
  SetOverwrite on 
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\es\DevExpress.Data.v18.2.resources.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\es\DevExpress.Pdf.v18.2.Core.resources.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\es\DevExpress.Printing.v18.2.Core.resources.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\es\DevExpress.Xpf.Controls.v18.2.resources.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\es\DevExpress.Xpf.Core.v18.2.resources.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\es\DevExpress.Xpf.Grid.v18.2.Core.resources.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\es\DevExpress.Xpf.LayoutControl.v18.2.resources.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\es\DevExpress.Xpf.Ribbon.v18.2.resources.dll"
  CreateDirectory "$SMPROGRAMS\Waton\SNet3.Monitor\es"
  
  SetShellVarContext all
  SetOutPath "$INSTDIR\ja"
  SetOverwrite on 
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\ja\DevExpress.Data.v18.2.resources.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\ja\DevExpress.Pdf.v18.2.Core.resources.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\ja\DevExpress.Printing.v18.2.Core.resources.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\ja\DevExpress.Xpf.Controls.v18.2.resources.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\ja\DevExpress.Xpf.Core.v18.2.resources.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\ja\DevExpress.Xpf.Grid.v18.2.Core.resources.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\ja\DevExpress.Xpf.LayoutControl.v18.2.resources.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\ja\DevExpress.Xpf.Ribbon.v18.2.resources.dll"
  CreateDirectory "$SMPROGRAMS\Waton\SNet3.Monitor\ja"   
  
  SetShellVarContext all
  SetOutPath "$INSTDIR\ru"
  SetOverwrite on 
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\ru\DevExpress.Data.v18.2.resources.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\ru\DevExpress.Pdf.v18.2.Core.resources.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\ru\DevExpress.Printing.v18.2.Core.resources.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\ru\DevExpress.Xpf.Controls.v18.2.resources.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\ru\DevExpress.Xpf.Core.v18.2.resources.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\ru\DevExpress.Xpf.Grid.v18.2.Core.resources.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\ru\DevExpress.Xpf.LayoutControl.v18.2.resources.dll"
  File "D:\Program Files (x86)\DevExpress 18.2\Components\Bin\Framework\ru\DevExpress.Xpf.Ribbon.v18.2.resources.dll"
  CreateDirectory "$SMPROGRAMS\Waton\SNet3.Monitor\ru"  

  SetOutPath "$INSTDIR"
  CreateShortCut "$SMPROGRAMS\Waton\SNet3.Monitor\SNet3.Monitor.lnk" "$INSTDIR\SNet3.Monitor.exe"
  CreateShortCut "$DESKTOP\SNet3.Monitor.lnk" "$INSTDIR\SNet3.Monitor.exe"
SectionEnd

Section -AdditionalIcons
  WriteIniStr "$INSTDIR\${PRODUCT_NAME}.url" "InternetShortcut" "URL" "${PRODUCT_WEB_SITE}"
  CreateShortCut "$SMPROGRAMS\Waton\SNet3.Monitor\Website.lnk" "$INSTDIR\${PRODUCT_NAME}.url"
  CreateShortCut "$SMPROGRAMS\Waton\SNet3.Monitor\Uninstall.lnk" "$INSTDIR\uninst.exe"
SectionEnd

Section -Post
  WriteUninstaller "$INSTDIR\uninst.exe"
  WriteRegStr HKLM "${PRODUCT_DIR_REGKEY}" "" "$INSTDIR\SNet3.Monitor.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayIcon" "$INSTDIR\SNet3.Monitor.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_WEB_SITE}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
SectionEnd

Function un.onUninstSuccess
  HideWindow
  ${Switch} $LANGUAGE
  ${Case} ${LANG_KOREAN}
    MessageBox MB_ICONINFORMATION|MB_OK "$(^Name)는 완전히 제거되었습니다."
  ${Break}
  ${Default}
    MessageBox MB_ICONINFORMATION|MB_OK "You have successfully uninstalled $(^Name)."
  ${Break}
  ${EndSwitch}
  
FunctionEnd

Function un.onInit
!insertmacro MUI_UNGETLANGUAGE
;  ${Switch} $LANGUAGE
;  ${Case} ${LANG_KOREAN}
;    MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "$(^Name)를 제거하시겠습니까?" IDYES +1
;  ${Break}
;  ${Default}
;    MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "Do you want to remove $(^Name)?" IDYES +2
;  ${Break}
;  ${EndSwitch}
;  MessageBox MB_OK "message1"
;  Abort
;  MessageBox MB_OK "message2"
FunctionEnd

Section Uninstall
  SetShellVarContext all
  
  Delete "$SMPROGRAMS\Waton\SNet3.Monitor\Uninstall.lnk"
  Delete "$SMPROGRAMS\Waton\SNet3.Monitor\Website.lnk"
  Delete "$SMPROGRAMS\Waton\SNet3.Monitor\SNet3.Monitor.lnk"
  Delete "$DESKTOP\SNet3.Monitor.lnk"
  RMDir "$SMPROGRAMS\Waton\SNet3.Monitor"

  RMDir /r "$INSTDIR"
  RMDir "$INSTDIR"

  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"
  DeleteRegKey HKLM "${PRODUCT_DIR_REGKEY}"
  SetAutoClose true
SectionEnd