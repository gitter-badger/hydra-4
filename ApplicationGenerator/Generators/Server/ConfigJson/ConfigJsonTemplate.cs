﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 15.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace AbstraX.Generators.Server.ConfigJson
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using Utils;
    using AbstraX.Generators;
    using AbstraX.Angular;
    using AbstraX.DataAnnotations;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\MC\RazorViewsDesigner\ApplicationGenerator\Generators\Server\ConfigJson\ConfigJsonTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    public partial class ConfigJsonTemplate : AbstraX.Generators.Base.TemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write("{\r\n  \"ClientKey\":\r\n  {\r\n    \"Id\": \"");
            
            #line 18 "D:\MC\RazorViewsDesigner\ApplicationGenerator\Generators\Server\ConfigJson\ConfigJsonTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.ClientId));
            
            #line default
            #line hidden
            this.Write("\",\r\n    \"Secret\": \"");
            
            #line 19 "D:\MC\RazorViewsDesigner\ApplicationGenerator\Generators\Server\ConfigJson\ConfigJsonTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.ClientSecret));
            
            #line default
            #line hidden
            this.Write("\"\r\n  },\r\n  \"Roles\":\r\n  [\r\n    {\r\n      \"Role\": \"Anonymous\",\r\n      \"Id\": \"{000000" +
                    "00-0000-0000-0000-000000000000}\"\r\n    }");
            
            #line 26 "D:\MC\RazorViewsDesigner\ApplicationGenerator\Generators\Server\ConfigJson\ConfigJsonTemplate.tt"


    foreach (var rolePair in this.Roles)
    {
 
            
            #line default
            #line hidden
            this.Write(",\r\n    {\r\n      \"Role\": \"");
            
            #line 32 "D:\MC\RazorViewsDesigner\ApplicationGenerator\Generators\Server\ConfigJson\ConfigJsonTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(rolePair.Value));
            
            #line default
            #line hidden
            this.Write("\",\r\n      \"Id\": \"{");
            
            #line 33 "D:\MC\RazorViewsDesigner\ApplicationGenerator\Generators\Server\ConfigJson\ConfigJsonTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(rolePair.Key.ToString()));
            
            #line default
            #line hidden
            this.Write("}\"\r\n    }\r\n");
            
            #line 35 "D:\MC\RazorViewsDesigner\ApplicationGenerator\Generators\Server\ConfigJson\ConfigJsonTemplate.tt"

    }

            
            #line default
            #line hidden
            this.Write("  ]\r\n}\r\n");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 1 "D:\MC\RazorViewsDesigner\ApplicationGenerator\Generators\Server\ConfigJson\ConfigJsonTemplate.tt"

private global::System.EventHandler _DebugCallbackField;

/// <summary>
/// Access the DebugCallback parameter of the template.
/// </summary>
private global::System.EventHandler DebugCallback
{
    get
    {
        return this._DebugCallbackField;
    }
}

private global::System.Collections.Generic.Dictionary<Guid, string> _RolesField;

/// <summary>
/// Access the Roles parameter of the template.
/// </summary>
private global::System.Collections.Generic.Dictionary<Guid, string> Roles
{
    get
    {
        return this._RolesField;
    }
}

private string _ClientIdField;

/// <summary>
/// Access the ClientId parameter of the template.
/// </summary>
private string ClientId
{
    get
    {
        return this._ClientIdField;
    }
}

private string _ClientSecretField;

/// <summary>
/// Access the ClientSecret parameter of the template.
/// </summary>
private string ClientSecret
{
    get
    {
        return this._ClientSecretField;
    }
}


/// <summary>
/// Initialize the template
/// </summary>
public override void Initialize()
{
    base.Initialize();
    if ((this.Errors.HasErrors == false))
    {
bool DebugCallbackValueAcquired = false;
if (this.Session.ContainsKey("DebugCallback"))
{
    this._DebugCallbackField = ((global::System.EventHandler)(this.Session["DebugCallback"]));
    DebugCallbackValueAcquired = true;
}
if ((DebugCallbackValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("DebugCallback");
    if ((data != null))
    {
        this._DebugCallbackField = ((global::System.EventHandler)(data));
    }
}
bool RolesValueAcquired = false;
if (this.Session.ContainsKey("Roles"))
{
    this._RolesField = ((global::System.Collections.Generic.Dictionary<Guid, string>)(this.Session["Roles"]));
    RolesValueAcquired = true;
}
if ((RolesValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("Roles");
    if ((data != null))
    {
        this._RolesField = ((global::System.Collections.Generic.Dictionary<Guid, string>)(data));
    }
}
bool ClientIdValueAcquired = false;
if (this.Session.ContainsKey("ClientId"))
{
    this._ClientIdField = ((string)(this.Session["ClientId"]));
    ClientIdValueAcquired = true;
}
if ((ClientIdValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("ClientId");
    if ((data != null))
    {
        this._ClientIdField = ((string)(data));
    }
}
bool ClientSecretValueAcquired = false;
if (this.Session.ContainsKey("ClientSecret"))
{
    this._ClientSecretField = ((string)(this.Session["ClientSecret"]));
    ClientSecretValueAcquired = true;
}
if ((ClientSecretValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("ClientSecret");
    if ((data != null))
    {
        this._ClientSecretField = ((string)(data));
    }
}


    }
}


        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
}
