﻿namespace Sitecore.MobileSDK.UrlBuilder.Children
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.SSC;
  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.Validators;

  public class ChildrenByIdUrlBuilder : GetItemsUrlBuilder<IReadItemsByIdRequest>
  {
    public ChildrenByIdUrlBuilder(IRestServiceGrammar restGrammar, ISSCUrlParameters sscGrammar)
      : base(restGrammar, sscGrammar)
    {
    }

    protected override string GetHostUrlForRequest(IReadItemsByIdRequest request)
    {
      string hostUrl = base.GetHostUrlForRequest(request);
      string itemId = UrlBuilderUtils.EscapeDataString(request.ItemId.ToLowerInvariant());

      string result = hostUrl + this.restGrammar.PathComponentSeparator + itemId + sscGrammar.ItemSSCChildrenAction;
      return result;
    }

    protected override string GetItemIdenticationForRequest(IReadItemsByIdRequest request)
    {
      return null;
    }

    protected override void ValidateSpecificRequest(IReadItemsByIdRequest request)
    {
      ItemIdValidator.ValidateItemId(request.ItemId, this.GetType().Name + ".ItemId");
    }
  }
}
