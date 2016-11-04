﻿using Sitecore.MobileSDK.API.Request.Parameters;

namespace Sitecore.MobileSDK.UserRequest
{
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Entity;
  using Sitecore.MobileSDK.Entities;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.Items.Delete;
  using Sitecore.MobileSDK.UrlBuilder.CreateItem;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;

  public class UserRequestMerger
  {
    public UserRequestMerger(ISessionConfig defaultSessionConfig, IItemSource defaultSource)
    {
      this.ItemSourceMerger = new ItemSourceFieldMerger(defaultSource);
      this.SessionConfigMerger = new SessionConfigMerger(defaultSessionConfig);
    }

    public IReadItemsByIdRequest FillReadItemByIdGaps(IReadItemsByIdRequest userRequest)
    {
      IItemSource mergedSource = this.ItemSourceMerger.FillItemSourceGaps(userRequest.ItemSource);
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionSettings);

      return new ReadItemsByIdParameters(
        mergedSessionConfig, 
        mergedSource, 
        userRequest.QueryParameters, 
        userRequest.IncludeStandardTemplateFields,
        userRequest.ItemId);
    }

    public ISitecoreStoredSearchRequest FillSitecoreStoredSearchGaps(ISitecoreStoredSearchRequest userRequest)
    {
      IItemSource mergedSource = this.ItemSourceMerger.FillItemSourceGaps(userRequest.ItemSource);
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionSettings);

      IPagingParameters pagingSettings = userRequest.PagingSettings;
      return new StoredSearchParameters(
        mergedSessionConfig, 
        mergedSource, 
        userRequest.QueryParameters, 
        pagingSettings,
        userRequest.ItemId,
        userRequest.IncludeStandardTemplateFields,
        userRequest.Term);
    }

    public ISitecoreSearchRequest FillSitecoreSearchGaps(ISitecoreSearchRequest userRequest)
    {
      IItemSource mergedSource = this.ItemSourceMerger.FillItemSourceGaps(userRequest.ItemSource);
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionSettings);

      IPagingParameters pagingSettings = userRequest.PagingSettings;
      return new SitecoreSearchParameters(
        mergedSessionConfig, 
        mergedSource, 
        userRequest.QueryParameters, 
        pagingSettings, 
        userRequest.SortParameters,
        userRequest.IncludeStandardTemplateFields,
        userRequest.Term);
    }

    public IReadItemsByPathRequest FillReadItemByPathGaps(IReadItemsByPathRequest userRequest)
    {
      IItemSource mergedSource = this.ItemSourceMerger.FillItemSourceGaps(userRequest.ItemSource);
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionSettings);

      return new ReadItemByPathParameters(
        mergedSessionConfig, 
        mergedSource, 
        userRequest.QueryParameters, 
        userRequest.IncludeStandardTemplateFields,
        userRequest.ItemPath);
    }

    public IMediaResourceDownloadRequest FillReadMediaItemGaps(IMediaResourceDownloadRequest userRequest)
    {
      IItemSource mergedSource = this.ItemSourceMerger.FillItemSourceGaps(userRequest.ItemSource);
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionSettings);

      return new MediaResourceDownloadParameters(mergedSessionConfig, mergedSource, userRequest.DownloadOptions, userRequest.MediaPath);
    }

    public ICreateItemByPathRequest FillCreateItemByPathGaps(ICreateItemByPathRequest userRequest)
    {
      IItemSource mergedSource = this.ItemSourceMerger.FillItemSourceGaps(userRequest.ItemSource);
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionSettings);
      CreateItemParameters createParams = new CreateItemParameters(userRequest.ItemName, userRequest.ItemTemplateId, userRequest.FieldsRawValuesByName);

      return new CreateItemByPathParameters(mergedSessionConfig, mergedSource, userRequest.QueryParameters, createParams, userRequest.ItemPath);
    }

    public IUpdateItemByIdRequest FillUpdateItemByIdGaps(IUpdateItemByIdRequest userRequest)
    {
      IItemSource mergedSource = this.ItemSourceMerger.FillItemSourceGaps(userRequest.ItemSource);
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionSettings);

      return new UpdateItemByIdParameters(mergedSessionConfig, mergedSource, userRequest.QueryParameters, userRequest.FieldsRawValuesByName, userRequest.ItemId);
    }

    public IUpdateItemByPathRequest FillUpdateItemByPathGaps(IUpdateItemByPathRequest userRequest)
    {
      IItemSource mergedSource = this.ItemSourceMerger.FillItemSourceGaps(userRequest.ItemSource);
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionSettings);

      return new UpdateItemByPathParameters(mergedSessionConfig, mergedSource, userRequest.QueryParameters, userRequest.FieldsRawValuesByName, userRequest.ItemPath);
    }

    public IDeleteItemsByIdRequest FillDeleteItemByIdGaps(IDeleteItemsByIdRequest userRequest)
    {
      string databse = userRequest.Database;
      if (string.IsNullOrEmpty(databse))
      {
        databse = this.ItemSourceMerger.DefaultSource.Database;
      }
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionConfig);

      return new DeleteItemByIdParameters(mergedSessionConfig, databse, userRequest.ItemId);
    }

    public IDeleteItemsByPathRequest FillDeleteItemByPathGaps(IDeleteItemsByPathRequest userRequest)
    {
      string databse = userRequest.Database;
      if (string.IsNullOrEmpty(databse))
      {
        databse = this.ItemSourceMerger.DefaultSource.Database;
      }
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionConfig);

      return new DeleteItemByPathParameters(mergedSessionConfig, databse, userRequest.ItemPath);
    }

    public IDeleteItemsByQueryRequest FillDeleteItemByQueryGaps(IDeleteItemsByQueryRequest userRequest)
    {
      string databse = userRequest.Database;
      if (string.IsNullOrEmpty(databse))
      {
        databse = this.ItemSourceMerger.DefaultSource.Database;
      }
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionConfig);

      return new DeleteItemByQueryParameters(mergedSessionConfig, databse, userRequest.SitecoreQuery);
    }

    #region Entity

    public IReadEntitiesByPathRequest FillReadEntitiesByPathGaps(IReadEntitiesByPathRequest userRequest)
    {
      //FIXME: @igk implement
      #warning FillReadEntitiesByPathGaps not implemented!


      EntitySource newSource = new EntitySource(userRequest.EntitySource.Namespase,
                                                userRequest.EntitySource.Controller,
                                                userRequest.EntitySource.Id,
                                                userRequest.EntitySource.Action);
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionSettings);

      ReadEntitiesByPathParameters newRequest = new ReadEntitiesByPathParameters(newSource, mergedSessionConfig);

      return newRequest;
    }

    public IReadEntityByIdRequest FillReadEntityByIdGaps(IReadEntityByIdRequest userRequest)
    {
      //FIXME: @igk implement
      #warning FillReadEntityByIdGaps not implemented!


      EntitySource newSource = new EntitySource(userRequest.EntitySource.Namespase,
                                                userRequest.EntitySource.Controller,
                                                userRequest.EntitySource.Id,
                                                userRequest.EntitySource.Action);
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionSettings);

      ReadEntityByIdParameters newRequest = new ReadEntityByIdParameters(userRequest.EntityID, newSource, mergedSessionConfig);

      return newRequest;
    }

    public ICreateEntityRequest FillCreateEntityGaps(ICreateEntityRequest userRequest)
    {
      //FIXME: @igk implement
#warning FillCreateEntityGaps not implemented!


      EntitySource newSource = new EntitySource(userRequest.EntitySource.Namespase,
                                                userRequest.EntitySource.Controller,
                                                userRequest.EntitySource.Id,
                                                userRequest.EntitySource.Action);

      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionSettings);

      CreateEntityParameters newRequest = new CreateEntityParameters(userRequest.EntityID, userRequest.FieldsRawValuesByName, newSource, mergedSessionConfig);

      return newRequest;

    }

    public IUpdateEntityRequest FillUpdateEntityGaps(IUpdateEntityRequest userRequest)
    {
      //FIXME: @igk implement
#warning FillUpdateEntityGaps not implemented!


      EntitySource newSource = new EntitySource(userRequest.EntitySource.Namespase,
                                                userRequest.EntitySource.Controller,
                                                userRequest.EntitySource.Id,
                                                userRequest.EntitySource.Action);

      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionSettings);

      UpdateEntityParameters newRequest = new UpdateEntityParameters(userRequest.EntityID, userRequest.FieldsRawValuesByName, newSource, mergedSessionConfig);

      return newRequest;
    }

    #endregion Entity

    public ItemSourceFieldMerger ItemSourceMerger { get; private set; }
    public SessionConfigMerger SessionConfigMerger { get; private set; }
  }
}

