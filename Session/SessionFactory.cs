﻿using System;
using nz.govt.moe.idp.saml.client.Utils;
using nz.govt.moe.idp.saml.client.config;
using nz.govt.moe.idp.saml.client.session.inproc;

namespace nz.govt.moe.idp.saml.client.session
{
    /// <summary>
    /// Factory for getting the concrete session implementation.
    /// </summary>
    class SessionFactory
    {
        static SessionFactory()
        {
            var type = FederationConfig.GetConfig().SessionType;
            if (!string.IsNullOrEmpty(type))
            {
                try
                {
                    var t = Type.GetType(type);
                    if (t != null)
                    {
                        Sessions = (ISessions)Activator.CreateInstance(t);
                    }
                    else
                    {
                        throw new Exception(string.Format("The type {0} is not available as session provider. Please check the type name and assembly", type));
                    }
                }
                catch (Exception e)
                {
                    Trace.TraceData(System.Diagnostics.TraceEventType.Critical, "Could not instantiate the configured session provider. Message: " + e.Message);
                    throw;
                }
            }
            else
            {
                Sessions = new InProcSessions();
            }
        }

        private static readonly ISessions Sessions;

        /// <summary>
        /// Returns the only instance of the session.
        /// </summary>
        internal static ISessions SessionContext
        {
            get { return Sessions; }
        }
    }
}
