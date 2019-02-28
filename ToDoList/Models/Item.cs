using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace ToDoList.Models
{
  public class Item
  {
    private string _description;
    private int _id;
    private int _categoryId;

    public Item (string description, int categoryId, int id = 0)
    {
      _description = description;
      _categoryId = categoryId;
      _id = id;
    }

    public string GetDescription()
    {
      return _description;
    }

    public void SetDescription(string newDescription)
    {
      _description = newDescription;
    }

    public int GetId()
    {
      return _id;
    }

    public static List<Item> GetAll()
    {
      List<Item> allItems = new List<Item> { };
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM items;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int itemId = rdr.GetInt32(0);
        string itemDescription = rdr.GetString(1);
        int itemCategoryId = rdr.GetInt32(2);
        Item newItem = new Item(itemDescription, itemCategoryId, itemId);
        allItems.Add(newItem);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allItems;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM items;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
       conn.Dispose();
      }
    }

    public static void ClearAllWithin(int categoryId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM items WHERE category_id = "+categoryId+";";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
       conn.Dispose();
      }
    }

    public static Item Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `items` WHERE id = (@searchId);";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int itemId = 0;
      string itemName = "";
      int itemCategoryId = 0;
      while (rdr.Read())
      {
         itemId = rdr.GetInt32(0);
         itemName = rdr.GetString(1);
         itemCategoryId = rdr.GetInt32(2);
      }
      Item newItem = new Item(itemName, itemCategoryId, itemId);
       conn.Close();
       if (conn != null)
       {
         conn.Dispose();
       }
      return newItem;
    }

    public override bool Equals(System.Object otherItem)
    {
      if (!(otherItem is Item))
      {
        return false;
      }
      else
      {
        Item newItem = (Item) otherItem;
        bool idEquality = (this.GetId() == newItem.GetId());
        bool descriptionEquality = (this.GetDescription() == newItem.GetDescription());
        bool categoryEquality = this.GetCategoryId() == newItem.GetCategoryId();
        return (idEquality && descriptionEquality);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO items (description, category_id) VALUES (@description, @category_id);";
      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "description";
      description.Value = this._description;
      cmd.Parameters.Add(description);
      MySqlParameter categoryId = new MySqlParameter();
      categoryId.ParameterName = "@category_id";
      categoryId.Value = this._categoryId;
      cmd.Parameters.Add(categoryId);
      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
       conn.Close();
       if (conn != null)
       {
         conn.Dispose();
       }
    }

    public void Edit(string newDescription)
    {
      //CREATES AND OPENS DATABASE CONNECTION
      MySqlConnection conn = DB.Connection();
      conn.Open();

      //CREATES MYSQLCOMMAND OBJECT AND SETS COMMANDTEXT PROPERTY TO THE SQL STATEMENT TO UPDATE ITEM
      //ALSO CREATES TWO PARAMETER PLACEHOLDERS FOR NEWDESCRIPTION AND SEARCHID
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE items SET description = @newDescription WHERE id = @searchId;";

      //CREATES MYSQL PARAMETERS TO FILL @NEWDESCRIPTION AND @SEARCHID
      //declare MySqlParameter objects for both searchId and description.
      //ASSOCIATE BOTH WITH THE SQL STATEMENT ABOVE
      //WE GIVE SEARCHID THE VALUE OF _ID (ITS ACTUAL ID NUMBER)
      //WE GIVE NEWDESCRIPTION VALUE OF NEWDESCRIPTION(REPRESENTS UPDATES DESCRIPTION VALUE PASSED INTO UPDATEDESCRIPTION AS AN ARGUMENT)
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);
      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@newDescription";
      description.Value = newDescription;
      cmd.Parameters.Add(description);

      //SINCE THIS METHOD MODIFIES DATA, YOU CALL ExecuteNonQuery INSTEAD OF EXECUTE READER
      cmd.ExecuteNonQuery();

      //RESETS ITEM'S _description PROPERTY
      _description = newDescription;

      //CLOSES DATABASE CONNECTION
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static void Delete(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      // string from = "FROM";
      // cmd.CommandText = @"DELETE "+from+" items WHERE id = "+id+";";
      // "This is a "+animal+" and it "+animalNoise+"."
      cmd.CommandText =@"DELETE FROM items WHERE id = "+id+";";
      // MySqlParameter thisId = new MySqlParameter();
      // thisId.ParameterName = "@thisId";
      // thisId.Value = itemId;
      // cmd.Parameters.Add(thisId);

      cmd.ExecuteNonQuery();

      conn.Close();
      {
        if (conn != null)
        {
          conn.Dispose();
        }
      }
    }

    public int GetCategoryId()
    {
      return _categoryId;
    }

  }
}
