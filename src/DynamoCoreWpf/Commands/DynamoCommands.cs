using System;
using System.Collections.Generic;
using System.Windows.Input;
using Dynamo.Models;
using Dynamo.Selection;
using Dynamo.UI.Commands;
using Dynamo.Utilities;

namespace Dynamo.ViewModels
{
    partial class DynamoViewModel
    {
        // Automation related data members.
        private AutomationSettings automationSettings = null;

        #region Automation Related Methods

        /// <summary>
        /// DynamoView calls this method at the end of its initialization
        /// sequence so that loaded commands, if any, begin to playback.
        /// </summary>
        internal void BeginCommandPlayback(System.Windows.Window mainWindow)
        {
            if (null != automationSettings)
                automationSettings.BeginCommandPlayback(mainWindow);
        }

        private void SaveRecordedCommands(object parameters)
        {
            if (null != automationSettings)
            {
                string xmlFilePath = automationSettings.SaveRecordedCommands();
                if (string.IsNullOrEmpty(xmlFilePath) == false)
                {
                    if (System.IO.File.Exists(xmlFilePath))
                        System.Diagnostics.Process.Start(xmlFilePath);
                }
            }
        }

        private bool CanSaveRecordedCommands(object parameters)
        {
            if (null == automationSettings)
                return false;

            return automationSettings.CanSaveRecordedCommands;
        }

        private void ExecInsertPausePlaybackCommand(object parameters)
        {
            if (automationSettings != null)
            {
                var msg = string.Format("PausePlaybackCommand '{0}' inserted",
                    automationSettings.InsertPausePlaybackCommand());
                model.Logger.Log(msg);
            }
        }

        private bool CanInsertPausePlaybackCommand(object parameters)
        {
            if (null == automationSettings)
                return false;

            return (automationSettings.CurrentState == AutomationSettings.State.Recording);
        }

        #endregion

        #region Workspace Command Entry Point

        public void ExecuteCommand(RecordableCommand command, bool executeOnly = false)
        {
            if (!executeOnly)
            {
                if (null != this.automationSettings)
                    this.automationSettings.RecordCommand(command);

                if (Model.DebugSettings.VerboseLogging)
                    model.Logger.Log("Command: " + command);
            }

            ExecuteCommandImpl(command as dynamic);
        }

        #endregion

        #region The Actual Command Handlers (Private)

        private void ExecuteCommandImpl(OpenFileCommand command)
        {
            this.VisualizationManager.Pause();

            model.OpenFileImpl(command);
            
            this.AddToRecentFiles(command.XmlFilePath);
            this.VisualizationManager.UnPause();
        }

        private void ExecuteCommandImpl(RunCancelCommand command)
        {
            model.RunCancelImpl(command);
        }

        private void ExecuteCommandImpl(ForceRunCancelCommand command)
        {
            model.ForceRunCancelImpl(command);
        }

        private void ExecuteCommandImpl(MutateTestCommand command)
        {
            var mutatorDriver = new Dynamo.TestInfrastructure.MutatorDriver(this);
            mutatorDriver.RunMutationTests();
        }

        private void ExecuteCommandImpl(CreateNodeCommand command)
        {
            model.CreateNodeImpl(command);

            UndoCommand.RaiseCanExecuteChanged();
            RedoCommand.RaiseCanExecuteChanged();
        }

        private void ExecuteCommandImpl(CreateNoteCommand command)
        {
            model.CreateNoteImpl(command);

            UndoCommand.RaiseCanExecuteChanged();
            RedoCommand.RaiseCanExecuteChanged();
        }

        private void ExecuteCommandImpl(SelectModelCommand command)
        {
            model.SelectModelImpl(command);
        }

        private void ExecuteCommandImpl(SelectInRegionCommand command)
        {
            CurrentSpaceViewModel.SelectInRegion(command.Region, command.IsCrossSelection);
        }

        private void ExecuteCommandImpl(DragSelectionCommand command)
        {
            if (DragSelectionCommand.Operation.BeginDrag == command.DragOperation)
                CurrentSpaceViewModel.BeginDragSelection(command.MouseCursor);
            else
                CurrentSpaceViewModel.EndDragSelection(command.MouseCursor);
        }

        private void ExecuteCommandImpl(MakeConnectionCommand command)
        {
            System.Guid nodeId = command.NodeId;

            switch (command.ConnectionMode)
            {
                case MakeConnectionCommand.Mode.Begin:
                    CurrentSpaceViewModel.BeginConnection(
                        nodeId, command.PortIndex, command.Type);
                    break;

                case MakeConnectionCommand.Mode.End:
                    CurrentSpaceViewModel.EndConnection(
                        nodeId, command.PortIndex, command.Type);
                    break;

                case MakeConnectionCommand.Mode.Cancel:
                    CurrentSpaceViewModel.CancelConnection();
                    break;
            }
        }

        private void ExecuteCommandImpl(DeleteModelCommand command)
        {
            model.DeleteModelImpl(command);

            UndoCommand.RaiseCanExecuteChanged();
            RedoCommand.RaiseCanExecuteChanged();
        }

        private void ExecuteCommandImpl(UndoRedoCommand command)
        {
            model.UndoRedoImpl(command);

            UndoCommand.RaiseCanExecuteChanged();
            RedoCommand.RaiseCanExecuteChanged();
        }

        private void ExecuteCommandImpl(ModelEventCommand command)
        {
            model.SendModelEventImpl(command);

            UndoCommand.RaiseCanExecuteChanged();
            RedoCommand.RaiseCanExecuteChanged();
        }

        private void ExecuteCommandImpl(UpdateModelValueCommand command)
        {
            model.UpdateModelValueImpl(command);

            UndoCommand.RaiseCanExecuteChanged();
            RedoCommand.RaiseCanExecuteChanged();
        }

        private void ExecuteCommandImpl(ConvertNodesToCodeCommand command)
        {
            model.ConvertNodesToCodeImpl(command);

            UndoCommand.RaiseCanExecuteChanged();
            RedoCommand.RaiseCanExecuteChanged();
        }

        private void ExecuteCommandImpl(CreateCustomNodeCommand command)
        {
            model.CreateCustomNodeImpl(command);
        }

        private void ExecuteCommandImpl(SwitchTabCommand command)
        {
            model.SwitchTabImpl(command);

            if (command.IsInPlaybackMode)
                RaisePropertyChanged("CurrentWorkspaceIndex");
        }

        #endregion

    }
}
