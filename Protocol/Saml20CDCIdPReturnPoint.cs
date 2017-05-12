﻿using System;
using System.Web;
using NZ.MOE.IdP.SAML.Client.session;
using NZ.MOE.IdP.SAML.Client.config;
using NZ.MOE.IdP.SAML.Client.protocol;
using NZ.MOE.IdP.SAML.Client.Utils;

namespace NZ.MOE.IdP.SAML.Client.protocol
{
    /// <summary>
    /// 
    /// </summary>
    public class Saml20CDCIdPReturnPoint : AbstractEndpointHandler
    {
        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler"/> interface.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpContext"/> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
        public override void ProcessRequest(HttpContext context)
        {
            try
            {
                Trace.TraceMethodCalled(GetType(), "ProcessRequest()");
                SAML20FederationConfig config = SAML20FederationConfig.GetConfig();

                if (config == null)
                    throw new Saml20Exception("Missing SAML20Federation config section in web.config.");

                Saml20ServiceEndpoint endp
                    = config.ServiceProvider.serviceEndpoints.Find(delegate(Saml20ServiceEndpoint ep) { return ep.endpointType == EndpointType.SIGNON; });

                if (endp == null)
                    throw new Saml20Exception("Signon endpoint not found in configuration");

                string redirectUrl = (string)SessionFactory.SessionContext.Current[SessionConstants.RedirectUrl];

                if (!string.IsNullOrEmpty(redirectUrl))
                {
                    SessionFactory.SessionContext.Current.Remove(SessionConstants.RedirectUrl);
                    context.Response.Redirect(redirectUrl);
                }
                else if (string.IsNullOrEmpty(endp.RedirectUrl))
                {
                    context.Response.Redirect("~/");
                }
                else
                {
                    context.Response.Redirect(endp.RedirectUrl);
                }
            }
            catch (Exception ex)
            {
                HandleError(context, ex);
            }
        }
    }
}
