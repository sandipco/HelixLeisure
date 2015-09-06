using HLeisure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace HLeisure.App_Start
{

    public class ProductModelBinder : IModelBinder
    {

        static ProductModelBinder()
        {

        }

        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
          //  if (bindingContext.ModelType != typeof(EntryModel))
          //  {
          //      return false;
          //  }

          //  var model = new EntryModel();

          //  var idValue = bindingContext.ValueProvider.GetValue("id");
          //  if (idValue != null)
          //  {

          //      model.Id = idValue.AttemptedValue;
          //  }
          //  var timestampValue = bindingContext.ValueProvider.GetValue("timestamp");
          //  if (timestampValue != null)
          //  {

          //      model.Timestamp = TimeSpan.Parse(timestampValue.AttemptedValue);
          //  }
          //  var salesPersonNameValue = bindingContext.ValueProvider.GetValue("sales_person_name");
          //  if (salesPersonNameValue != null)
          //  {
          //      model.SalesPersonName = salesPersonNameValue.AttemptedValue;
          //  }
          //  var locationNameValue = bindingContext.ValueProvider.GetValue("location_name");
          //  if (locationNameValue != null)
          //  {

          //      model.LocationName = locationNameValue.AttemptedValue;
          //  }

          //  var totalSaleAmountValue = bindingContext.ValueProvider.GetValue("total_sale_amount");
          //  if (totalSaleAmountValue != null)
          //  {
          //      double totalSalesAmount = 0;
          //      double.TryParse(totalSaleAmountValue.AttemptedValue, out totalSalesAmount);
          //      model.TotalAmount = totalSalesAmount;
          //  }
          //  var index=0;
          //  model.SalesDetails = new List<SalesDetailModel>();
          //  var found=true;
          //  do{
          //      var tmp=new SalesDetailModel();
               
          //      var pName =string.Format("products[{0}]",index);
          //      if (bindingContext.ValueProvider.ContainsPrefix(pName))
          //      {
          //          pName = string.Format("products[{0}].name", index);
          //          if (bindingContext.ValueProvider.ContainsPrefix(pName))
          //          {
          //              var pValue = bindingContext.ValueProvider.GetValue(pName);
          //              if (!string.IsNullOrEmpty(pValue.AttemptedValue))
          //              {
          //                  tmp.Name = pValue.AttemptedValue;
          //              }
          //          }
                    
          //          pName = string.Format("products[{0}].quantity", index);
          //          if (bindingContext.ValueProvider.ContainsPrefix(pName))
          //          {
          //             var pValue = bindingContext.ValueProvider.GetValue(pName);
          //              if (!string.IsNullOrEmpty(pValue.AttemptedValue))
          //              {
          //                  var quantity = 0;
          //                  int.TryParse(pValue.AttemptedValue, out quantity);
          //                  tmp.Quantity = quantity;
          //              }
          //          }

          //          pName = string.Format("products[{0}].sale_amount", index);
          //          if (bindingContext.ValueProvider.ContainsPrefix(pName))
          //          {
          //             var pValue = bindingContext.ValueProvider.GetValue(pName);
          //              if (!string.IsNullOrEmpty(pValue.AttemptedValue))
          //              {
          //                  double saleAmount = 0;
          //                  double.TryParse(pValue.AttemptedValue, out saleAmount);
          //                  tmp.Total = saleAmount;
          //              }
          //          }

          //          index++;
          //          model.SalesDetails.Add(tmp);
          //      }
          //      else
          //      {
          //          found = false;
          //      }

          //  }while(found);
          ////  var productName="proc"
           
          //  bindingContext.Model = model;

            return true;
        }

     
    }
}