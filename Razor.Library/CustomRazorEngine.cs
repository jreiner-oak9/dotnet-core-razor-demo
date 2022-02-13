﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Razor.Library
{
    // NOTE: This would be useful in an ASP.NET app (like our API), but I used a NuGet package to do the dirty work for this non-ASP.NET console app.
    public class CustomRazorEngine
    {
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;

        public CustomRazorEngine(
            IRazorViewEngine razorViewEngine,
            ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider
            )
        {
            _razorViewEngine = razorViewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
        }

        private IView FindView(string ViewName)
        {
            ViewEngineResult viewResult = _razorViewEngine.GetView(executingFilePath: null, viewPath: ViewName, isMainPage: true);
            if (viewResult.Success)
            {
                return viewResult.View;
            }
            throw new Exception("Invalid View Path");
        }

        private ActionContext GetContext()
        {
            var httpContext = new DefaultHttpContext
            {
                RequestServices = _serviceProvider
            };
            return new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
        }

        public async Task<string> RazorViewToHtmlAsync<TModel>(string viewName, TModel model)
        {
            var actionContext = GetContext();
            var view = FindView(viewName);

            using (var output = new StringWriter())
            {
                var viewContext = new ViewContext(
                    actionContext: actionContext,
                    view: view,
                    viewData: new ViewDataDictionary<TModel>(
                        metadataProvider: new EmptyModelMetadataProvider(),
                        modelState: new ModelStateDictionary()
                        )
                    {
                        Model = model
                    },
                    tempData: new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                    writer: output,
                    htmlHelperOptions: new HtmlHelperOptions()
                    );
                await view.RenderAsync(viewContext);
                return output.ToString();
            }
        }
    }
}
