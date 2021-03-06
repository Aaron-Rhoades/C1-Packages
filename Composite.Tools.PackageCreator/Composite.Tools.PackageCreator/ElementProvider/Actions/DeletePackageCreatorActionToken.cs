using System.Collections.Generic;
using Composite.C1Console.Actions;
using Composite.C1Console.Security;
using Composite.Tools.PackageCreator.ElementProvider.EntityTokens;

namespace Composite.Tools.PackageCreator.ElementProvider.Actions
{
    [ActionExecutor(typeof(DeleteConfigPackageCreatorElementProviderActionExecutor))]
    public sealed class DeleteConfigPackageCreatorActionToken : ActionToken
    {
        static private IEnumerable<PermissionType> _permissionTypes = new PermissionType[] { PermissionType.Administrate };

        public override IEnumerable<PermissionType> PermissionTypes
        {
            get { return _permissionTypes; }
        }

        public override string Serialize()
        {
            return "DeleteConfigPackageCreator";
        }

        public static ActionToken Deserialize(string serializedData)
        {
            return new DeleteConfigPackageCreatorActionToken();
        }
    }

    public sealed class DeleteConfigPackageCreatorElementProviderActionExecutor : IActionExecutor
    {

        public FlowToken Execute(EntityToken entityToken, ActionToken actionToken, FlowControllerServicesContainer flowControllerServicesContainer)
        {
            PackageCreatorFacade.DeletePackageInformation(entityToken.Source);
            SpecificTreeRefresher treeRefresher = new SpecificTreeRefresher(flowControllerServicesContainer);
            treeRefresher.PostRefreshMesseges(new PackageCreatorElementProviderEntityToken());
            return null;
        }
    }
}
