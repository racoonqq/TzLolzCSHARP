using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace DBMethods
{
    public class Methods
    {
        private SQLiteConnection CONN = Database.DbManager.GetConnection();
        public void ActionGame(string data)
        {

            if (data.Split(' ').Length == 4)
            {
                string[] Info = data.Split(' ');
                    try
                    {
                    using (var comanda = CONN.CreateCommand())
                    {
                        if (Info[3] == "I")
                        {
                            comanda.CommandText = "INSERT INTO all_games (name, companyName, CreatedAt) VALUES (@name, @companyName, @createdAt)";
                        } else
                        {
                            comanda.CommandText = "UPDATE all_games SET name = @name, companyName = @companyName, CreatedAt = @createdAt WHERE id = @Id";
                            comanda.Parameters.AddWithValue("@Id", Info[3]);
                        }
                        comanda.Parameters.AddWithValue("@name", Info[0]);
                        comanda.Parameters.AddWithValue("@companyName", Info[1]);
                        comanda.Parameters.AddWithValue("@createdAt", Info[2]);
                        comanda.ExecuteNonQuery();
                        Console.WriteLine("Игру добавлено");
                    }
                    } catch(Exception ex)
                    {
                    if (ex.ToString().Contains("UNIQUE"))
                    {
                        Console.WriteLine("Игра с таким названием уже существует");
                    }
                    else
                    {
                        Console.Write(ex.ToString());
                    }
                    }
                  
            } else
            {
                Error();
            }
        }

        async public void FindGame(string data) {
            using(var comanda = CONN.CreateCommand())
            {
                try
                {
                    comanda.CommandText = "SELECT * FROM all_games WHERE name LIKE @name OR companyName LIKE @companyName OR CreatedAt LIKE @CreatedAt";
                    comanda.Parameters.AddWithValue("@name", '%' +data+ '%');
                    comanda.Parameters.AddWithValue("@companyName", '%' +data+ '%');
                    comanda.Parameters.AddWithValue("@CreatedAt", '%' +data+ '%');

                    //var Game = await comanda.ExecuteReaderAsync();

                    using (var R = await comanda.ExecuteReaderAsync())
                    {
                        while(R.Read())
                        {
                            Console.Write("Айди : " + R["id"] + "; " + "Имя : " + R["name"] + "; " + " Издатель : " + R["companyName"] + "; " + "Год выпуска : " + R["CreatedAt"] + ";" + "\n");
                        }
                    }
                } catch(Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
        }

        public void DeleteGame(int Id)
        {
            using(var comanda = CONN.CreateCommand())
            {
                try
                {
                    comanda.CommandText = "DELETE FROM all_games WHERE id= @Id";
                    comanda.Parameters.AddWithValue("@Id", Id);

                    int rowAffected = comanda.ExecuteNonQuery();

                    if (rowAffected > 0)
                    {
                        Console.WriteLine("Игра удалена");
                    }
                    else
                    {
                        Console.WriteLine("Игра не найдена");
                    }
                }
                catch (Exception ex) {
                    Console.Write(ex.Message + "\n");
                }
            }
        }

        public void AllGames()
        {
            using (var comanda = CONN.CreateCommand())
            {
                try
                {
                    comanda.CommandText = "SELECT * FROM all_games";
                    using (var R = comanda.ExecuteReader())
                    {
                        while (R.Read())
                        {
                            Console.WriteLine("Айди : " + R["id"] + "; " + "Название : " + R["name"] + "; " + "Издатель : " + R["companyName"] + "; " + "Дата выпуска : " + R["CreatedAt"] + ";");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
        }

        public void Error()
        {
            Console.WriteLine("Некоректно заполненые данные");
        }
    }
}
