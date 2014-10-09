using System.Windows.Input;

using Dynamo.UI.Commands;

namespace Dynamo.ViewModels
{
    public interface IWatchViewModel
    {
        IDelegateCommand GetBranchVisualizationCommand { get; set; }
        bool WatchIsResizable { get; set; }
        IDelegateCommand CheckForLatestRenderCommand { get; set; }
    }
}
