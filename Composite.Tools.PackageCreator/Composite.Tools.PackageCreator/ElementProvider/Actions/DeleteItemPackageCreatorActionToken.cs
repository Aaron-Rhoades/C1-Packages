using System.Collections.Generic;
using Composite.C1Console.Actions;
using Composite.C1Console.Security;
using Composite.Tools.PackageCreator.Actions;
using Composite.Tools.PackageCreator.ElementProvider.EntityTokens;
using Composite.Tools.PackageCreator.Types;

namespace Composite.Tools.PackageCreator.ElementProvider.Actions
{
    [ActionExecutor(typeof(DeleteItemPackageCreatorElementProviderActionExecutor))]
    public sealed class DeleteItemPackageCreatorActionToken : ActionToken
    {
        static private IEnumerable<PermissionType> _permissionTypes = new PermissionType[] { PermissionType.Administrate };

        public override IEnumerable<PermissionType> PermissionTypes
        {
            get { return _permissionTypes; }
        }

        public override string Serialize()
        {
            return "DeleteItemPackageCreator";
        }

        public static ActionToken Deserialize(string serializedData)
        {
            return new DeleteItemPackageCreatorActionToken();
        }
    }

    public class DeleteItemPackageCreatorElementProviderActionExecutor : IActionExecutor
    {
        public FlowToken Execute(EntityToken entityToken, ActionToken actionToken, FlowControllerServicesContainer flowControllerServicesContainer)
        {
            var item = PackageCreatorActionFacade.GetPackageCreatorItem(entityToken.Type, entityToken.Id);
			PackageCreatorFacade.RemoveItem(item, entityToken.Source);

            SpecificTreeRefresher treeRefresher = new SpecificTreeRefresher(flowControllerServicesContainer);
            treeRefresher.PostRefreshMesseges(new PackageCreatorElementProviderEntityToken());

	        var itemToggle = item as IPackToggle;
	        if (itemToggle != null)
	        {
		        var itemEntityToken = itemToggle.GetEntityToken();
				if (itemEntityToken != null)
		        {
					var parentTreeRefresher = new ParentTreeRefresher(flowControllerServicesContainer);
					parentTreeRefresher.PostRefreshMesseges(itemEntityToken);
		        }
	        }

	        return null;
        }
    }
}
