using System.Windows.Input;

using Dynamo.UI.Commands;

namespace Dynamo.ViewModels
{
    partial class DynamoViewModel
    {
        private void InitializeDelegateCommands()
        {
            OpenCommand = new DelegateCommand(Open, CanOpen);
            OpenRecentCommand = new DelegateCommand(OpenRecent, CanOpenRecent);
            SaveCommand = new DelegateCommand(Save, CanSave);
            SaveAsCommand = new DelegateCommand(SaveAs, CanSaveAs);
            ShowOpenDialogAndOpenResultCommand = new DelegateCommand(ShowOpenDialogAndOpenResult, CanShowOpenDialogAndOpenResultCommand);
            ShowSaveDialogAndSaveResultCommand = new DelegateCommand(ShowSaveDialogAndSaveResult, CanShowSaveDialogAndSaveResult);
            ShowSaveDialogIfNeededAndSaveResultCommand = new DelegateCommand(ShowSaveDialogIfNeededAndSaveResult, CanShowSaveDialogIfNeededAndSaveResultCommand);
            SaveImageCommand = new DelegateCommand(SaveImage, CanSaveImage);
            ShowSaveImageDialogAndSaveResultCommand = new DelegateCommand(ShowSaveImageDialogAndSaveResult, CanShowSaveImageDialogAndSaveResult);

            WriteToLogCmd = new DelegateCommand(model.WriteToLog, CanWriteToLog);
            PostUiActivationCommand = new DelegateCommand(model.PostUIActivation, model.CanDoPostUIActivation);
            AddNoteCommand = new DelegateCommand(AddNote, CanAddNote);
            AddToSelectionCommand = new DelegateCommand(model.AddToSelection, CanAddToSelection);
            ShowNewFunctionDialogCommand = new DelegateCommand(ShowNewFunctionDialogAndMakeFunction, CanShowNewFunctionDialogCommand);
            SaveRecordedCommand = new DelegateCommand(SaveRecordedCommands, CanSaveRecordedCommands);
            InsertPausePlaybackCommand = new DelegateCommand(ExecInsertPausePlaybackCommand, CanInsertPausePlaybackCommand);
            GraphAutoLayoutCommand = new DelegateCommand(DoGraphAutoLayout, CanDoGraphAutoLayout);
            GoHomeCommand = new DelegateCommand(GoHomeView, CanGoHomeView);
            SelectAllCommand = new DelegateCommand(SelectAll, CanSelectAll);
            HomeCommand = new DelegateCommand(model.Home, model.CanGoHome);
            NewHomeWorkspaceCommand = new DelegateCommand(MakeNewHomeWorkspace, CanMakeNewHomeWorkspace);
            CloseHomeWorkspaceCommand = new DelegateCommand(CloseHomeWorkspace, CanCloseHomeWorkspace);
            GoToWorkspaceCommand = new DelegateCommand(GoToWorkspace, CanGoToWorkspace);
            DeleteCommand = new DelegateCommand(Delete, CanDelete);
            ExitCommand = new DelegateCommand(Exit, CanExit);
            ToggleFullscreenWatchShowingCommand = new DelegateCommand(ToggleFullscreenWatchShowing, CanToggleFullscreenWatchShowing);
            ToggleCanNavigateBackgroundCommand = new DelegateCommand(ToggleCanNavigateBackground, CanToggleCanNavigateBackground);
            AlignSelectedCommand = new DelegateCommand(AlignSelected, CanAlignSelected); ;
            UndoCommand = new DelegateCommand(Undo, CanUndo);
            RedoCommand = new DelegateCommand(Redo, CanRedo);
            CopyCommand = new DelegateCommand(model.Copy, CanCopy);
            PasteCommand = new DelegateCommand(model.Paste, CanPaste);
            ToggleConsoleShowingCommand = new DelegateCommand(ToggleConsoleShowing, CanToggleConsoleShowing);
            CancelRunCommand = new DelegateCommand(CancelRunCmd, CanCancelRunCmd);
            RunExpressionCommand = new DelegateCommand(RunExprCmd, CanRunExprCmd);
            ForceRunExpressionCommand = new DelegateCommand(ForceRunExprCmd, CanRunExprCmd);
            MutateTestDelegateCommand = new DelegateCommand(MutateTestCmd, CanRunExprCmd);
            DisplayFunctionCommand = new DelegateCommand(DisplayFunction, CanDisplayFunction);
            SetConnectorTypeCommand = new DelegateCommand(SetConnectorType, CanSetConnectorType);
            ReportABugCommand = new DelegateCommand(ReportABug, CanReportABug);
            GoToWikiCommand = new DelegateCommand(GoToWiki, CanGoToWiki);
            GoToSourceCodeCommand = new DelegateCommand(GoToSourceCode, CanGoToSourceCode);
            DisplayStartPageCommand = new DelegateCommand(DisplayStartPage, CanDisplayStartPage);
            ShowPackageManagerSearchCommand = new DelegateCommand(ShowPackageManagerSearch, CanShowPackageManagerSearch);
            PublishNewPackageCommand = new VoidDelegateCommand(PackageManagerClientViewModel.PublishNewPackage, PackageManagerClientViewModel.CanPublishNewPackage);
            ShowInstalledPackagesCommand = new DelegateCommand(ShowInstalledPackages, CanShowInstalledPackages);
            PublishCurrentWorkspaceCommand = new VoidDelegateCommand(PackageManagerClientViewModel.PublishCurrentWorkspace, PackageManagerClientViewModel.CanPublishCurrentWorkspace);
            PublishSelectedNodesCommand = new VoidDelegateCommand(PackageManagerClientViewModel.PublishSelectedNodes, PackageManagerClientViewModel.CanPublishSelectedNodes);
            ShowHideConnectorsCommand = new DelegateCommand(ShowConnectors, CanShowConnectors);
            SelectNeighborsCommand = new DelegateCommand(SelectNeighbors, CanSelectNeighbors);
            ClearLogCommand = new DelegateCommand(ClearLog, CanClearLog);
            PanCommand = new DelegateCommand(Pan, CanPan);
            ZoomInCommand = new DelegateCommand(ZoomIn, CanZoomIn);
            ZoomOutCommand = new DelegateCommand(ZoomOut, CanZoomOut);
            FitViewCommand = new DelegateCommand(FitView, CanFitView);
            TogglePanCommand = new DelegateCommand(TogglePan, CanTogglePan);
            ToggleOrbitCommand = new DelegateCommand(ToggleOrbit, CanToggleOrbit);
            EscapeCommand = new DelegateCommand(Escape, CanEscape);
            ExportToSTLCommand = new DelegateCommand(ExportToSTL, CanExportToSTL);
            ImportLibraryCommand = new DelegateCommand(ImportLibrary, CanImportLibrary);
            SetLengthUnitCommand = new DelegateCommand(SetLengthUnit, CanSetLengthUnit);
            SetAreaUnitCommand = new DelegateCommand(SetAreaUnit, CanSetAreaUnit);
            SetVolumeUnitCommand = new DelegateCommand(SetVolumeUnit, CanSetVolumeUnit);
            ShowAboutWindowCommand = new DelegateCommand(ShowAboutWindow, CanShowAboutWindow);
            SetNumberFormatCommand = new DelegateCommand(SetNumberFormat, CanSetNumberFormat);

            GetBranchVisualizationCommand = new DelegateCommand(GetBranchVisualization, CanGetBranchVisualization);
            CheckForLatestRenderCommand = new DelegateCommand(CheckForLatestRender, CanCheckForLatestRender);
        }

        public IDelegateCommand OpenCommand { get; set; }
        public IDelegateCommand ShowOpenDialogAndOpenResultCommand { get; set; }
        public IDelegateCommand WriteToLogCmd { get; set; }
        public IDelegateCommand PostUiActivationCommand { get; set; }
        public IDelegateCommand AddNoteCommand { get; set; }
        public IDelegateCommand UndoCommand { get; set; }
        public IDelegateCommand RedoCommand { get; set; }
        public IDelegateCommand CopyCommand { get; set; }
        public IDelegateCommand PasteCommand { get; set; }
        public IDelegateCommand AddToSelectionCommand { get; set; }
        public IDelegateCommand ShowNewFunctionDialogCommand { get; set; }
        public IDelegateCommand SaveRecordedCommand { get; set; }
        public IDelegateCommand InsertPausePlaybackCommand { get; set; }
        public IDelegateCommand GraphAutoLayoutCommand { get; set; }
        public IDelegateCommand GoHomeCommand { get; set; }
        public IDelegateCommand ShowPackageManagerSearchCommand { get; set; }
        public IDelegateCommand ShowInstalledPackagesCommand { get; set; }
        public IDelegateCommand HomeCommand { get; set; }
        public IDelegateCommand ExitCommand { get; set; }
        public IDelegateCommand ShowSaveDialogIfNeededAndSaveResultCommand { get; set; }
        public IDelegateCommand ShowSaveDialogAndSaveResultCommand { get; set; }
        public IDelegateCommand SaveCommand { get; set; }
        public IDelegateCommand SaveAsCommand { get; set; }
        public IDelegateCommand NewHomeWorkspaceCommand { get; set; }
        public IDelegateCommand CloseHomeWorkspaceCommand { get; set; }
        public IDelegateCommand GoToWorkspaceCommand { get; set; }
        public IDelegateCommand DeleteCommand { get; set; }
        public IDelegateCommand AlignSelectedCommand { get; set; }
        public IDelegateCommand PostUIActivationCommand { get; set; }
        public IDelegateCommand ToggleFullscreenWatchShowingCommand { get; set; }
        public IDelegateCommand ToggleCanNavigateBackgroundCommand { get; set; }
        public IDelegateCommand SelectAllCommand { get; set; }
        public IDelegateCommand SaveImageCommand { get; set; }
        public IDelegateCommand ShowSaveImageDialogAndSaveResultCommand { get; set; }
        public IDelegateCommand ToggleConsoleShowingCommand { get; set; }
        public IDelegateCommand ShowPackageManagerCommand { get; set; }
        public IDelegateCommand CancelRunCommand { get; set; }
        public IDelegateCommand RunExpressionCommand { get; set; }
        public IDelegateCommand ForceRunExpressionCommand { get; set; }
        public IDelegateCommand MutateTestDelegateCommand { get; set; }
        public IDelegateCommand DisplayFunctionCommand { get; set; }
        public IDelegateCommand SetConnectorTypeCommand { get; set; }
        public IDelegateCommand ReportABugCommand { get; set; }
        public IDelegateCommand GoToWikiCommand { get; set; }
        public IDelegateCommand GoToSourceCodeCommand { get; set; }
        public IDelegateCommand DisplayStartPageCommand { get; set; }
        public IDelegateCommand ShowHideConnectorsCommand { get; set; }
        public IDelegateCommand SelectNeighborsCommand { get; set; }
        public IDelegateCommand ClearLogCommand { get; set; }
        public IDelegateCommand SubmitCommand { get; set; }
        public IDelegateCommand PublishNewPackageCommand { get; set; }
        public IDelegateCommand PublishCurrentWorkspaceCommand { get; set; }
        public IDelegateCommand PublishSelectedNodesCommand { get; set; }
        public IDelegateCommand PanCommand { get; set; }
        public IDelegateCommand ZoomInCommand { get; set; }
        public IDelegateCommand ZoomOutCommand { get; set; }
        public IDelegateCommand FitViewCommand { get; set; }
        public IDelegateCommand TogglePanCommand { get; set; }
        public IDelegateCommand ToggleOrbitCommand { get; set; }
        public IDelegateCommand EscapeCommand { get; set; }
        public IDelegateCommand ExportToSTLCommand { get; set; }
        public IDelegateCommand ImportLibraryCommand { get; set; }
        public IDelegateCommand SetLengthUnitCommand { get; set; }
        public IDelegateCommand SetAreaUnitCommand { get; set; }
        public IDelegateCommand SetVolumeUnitCommand { get; set; }
        public IDelegateCommand ShowAboutWindowCommand { get; set; }
        public IDelegateCommand SetNumberFormatCommand { get; set; }
        public IDelegateCommand OpenRecentCommand { get; set; }
        public IDelegateCommand GetBranchVisualizationCommand { get; set; }
        public IDelegateCommand CheckForLatestRenderCommand { get; set; }
    }
}
