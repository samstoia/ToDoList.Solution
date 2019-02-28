// using Microsoft.AspNetCore.Mvc;
// using ToDoList.Models;
// using System.Collections.Generic;
//
// namespace ToDoList.Controllers
// {
//   public class ItemsController : Controller
//   {
//
//     [HttpGet("/categories/{categoryId}/items/new")]
//     public ActionResult New(int categoryId)
//     {
//        Category category = Category.Find(categoryId);
//        return View(category);
//     }
//
//     [HttpGet("/categories/{categoryId}/items/{itemId}")]
//     public ActionResult Show(int categoryId, int itemId)
//     {
//       Item item = Item.Find(itemId);
//       Dictionary<string, object> model = new Dictionary<string, object>();
//       Category category = Category.Find(categoryId);
//       model.Add("item", item);
//       model.Add("category", category);
//       return View(model);
//     }
//
//     [HttpPost("/items/delete")]
//     public ActionResult DeleteAll()
//     {
//       Item.ClearAll();
//       return View();
//     }
//
//     //CREATES RESTFUL EDIT ROUTE
//     //URL PATH INCLUDES ITEM ID AND CATEGORY ID
//     //PASSES THEM BOTH INTO EDIT ROUTE AS PARAMETERS
//     //USING FIND METHOD - WE LOCATE ITEM AND CATEGORY ADD THEM TO A Dictionary NAMED MODEL, AND PASS THE Dictionary INTO THE VIEW
//
//     [HttpGet("/categories/{categoryId}/items/{itemId}/edit")]
//     public ActionResult Edit(int categoryId, int itemId)
//     {
//       Dictionary<string, object> model = new Dictionary<string, object>();
//       Category category = Category.Find(categoryId);
//       model.Add("category", category);
//       Item item = Item.Find(itemId);
//       model.Add("item", item);
//       return View(model);
//     }
//
//     //Update method takes three  PARAMETERs
//     //edit method being called on
//     [HttpPost("/categories/{categoryId}/items/{itemId}")]
//     public ActionResult Update (int categoryId, int itemId, string newDescription)
//     {
//       Item item = Item.Find(itemId);
//       item.Edit(newDescription);
//       Dictionary<string, object> model = new Dictionary<string, object>();
//       Category category = Category.Find(categoryId);
//       model.Add("category", category);
//       model.Add("item", item);
//       return View("Show", model);
//     }
//   }
// }
