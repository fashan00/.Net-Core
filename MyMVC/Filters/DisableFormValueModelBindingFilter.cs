using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

// 由於要自行處理 Request 來的資料，所以要把 原本的 Model Binding 移除 。
// 建立一個 Attribute 註冊在大型檔案上傳的 API，透過 Resource Filter 在 Model Binding 之前把它移除。

namespace MyWebsite.Filters {
    [AttributeUsage (AttributeTargets.Class | AttributeTargets.Method)]
    public class DisableFormValueModelBindingFilter : Attribute, IResourceFilter {
        public void OnResourceExecuting (ResourceExecutingContext context) {
            var formValueProviderFactory = context.ValueProviderFactories
                .OfType<FormValueProviderFactory> ()
                .FirstOrDefault ();
            if (formValueProviderFactory != null) {
                context.ValueProviderFactories.Remove (formValueProviderFactory);
            }

            var jqueryFormValueProviderFactory = context.ValueProviderFactories
                .OfType<JQueryFormValueProviderFactory> ()
                .FirstOrDefault ();
            if (jqueryFormValueProviderFactory != null) {
                context.ValueProviderFactories.Remove (jqueryFormValueProviderFactory);
            }
        }

        public void OnResourceExecuted (ResourceExecutedContext context) { }
    }
}