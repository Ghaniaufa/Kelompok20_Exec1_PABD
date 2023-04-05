using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Kelompok20_Exec1_PABD
{
    class Program
    {
        static void Main(string[] args)
        {
            Program pr = new Program();
            while (true)
            {
                try
                {
                    Console.WriteLine("Koneksi Ke Database\n");
                    Console.WriteLine("Masukan User ID : ");
                    string user = Console.ReadLine();
                    Console.WriteLine("Masukan Password :");
                    string pass = Console.ReadLine();
                    Console.WriteLine("Masukkan Database Tujuan :");
                    string db = Console.ReadLine();
                    Console.Write("\nKetik K untuk Terhubung ke Database: ");
                    char chr = Convert.ToChar(Console.ReadLine());
                    switch (chr)
                    {
                        case 'K':
                            {
                                SqlConnection conn = null;
                                string strKoneksi = "Data source =MAULAGHANI\\GHANI; " + "initial catalog = {0};" + "User ID = {1}; password = {2}";
                                conn = new SqlConnection(string.Format(strKoneksi, db, user, pass));
                                conn.Open();
                                Console.Clear();
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("\nMenu");
                                        Console.WriteLine("1. Melihat Data Penjual");
                                        Console.WriteLine("2. Tambah Data");
                                        Console.WriteLine("3. Keluar");
                                        Console.Write("\nEnter Your Choice (1-3): ");
                                        char ch = Convert.ToChar(Console.ReadLine());
                                        switch (ch)
                                        {
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("DATA Penjual\n");
                                                    Console.WriteLine();
                                                    pr.baca(conn);
                                                    conn.Close();
                                                }
                                                break;
                                            case '2':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("INPUT DATA ORDERAN\n");
                                                    Console.WriteLine("Masukan DATA NO ORDERAN :");
                                                    string NoOrdr = Console.ReadLine();
                                                    Console.WriteLine("Masukan Id Game :");
                                                    string Idgm = Console.ReadLine();
                                                    Console.WriteLine("Masukan Tanggal : ");
                                                    string Tgl = Console.ReadLine();
                                                    Console.WriteLine("Masukan Id pembeli :");
                                                    string idp = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Id penjual : ");
                                                    string ip = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Id admin : ");
                                                    string ia = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.insert(NoOrdr, Idgm, Tgl, idp, ip, ia, conn);
                                                        conn.Close();
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("\nAnda tidak memiliki " + "Akses untuk menambah data");
                                                    }
                                                }
                                                break;
                                            case '3':
                                                conn.Close();
                                                return;
                                            default:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("\nInvalid option");
                                                }
                                                break;

                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("\nCheck for the value entered.");
                                    }
                                }
                            }
                        default:
                            {
                                Console.WriteLine("\nInvalid Option");
                            }
                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tidak Dapat Mengakses Database Menggunakan User Tersebut\n");
                }
            }
        }
        public void baca(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("Select * From DBO.Penjual",con);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                for (int i = 0; i < r.FieldCount; i++)
                {
                    Console.WriteLine(r.GetValue(i));
                }
                Console.WriteLine();
            }
            r.Close();
        }
        public void insert(string NoOrder, string Idgm, string Tgl, string ip, string idp, string ia, SqlConnection con)
        {
            string str = "";
            str = "Insert into DBO.Orderan (NoOrder,Idgm,Tgl,idp,ip,ia)" + " values(@No Order,@Id Game,@Tanggal,@Id penjual,@Id pembeli,@Id admin)";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("No Order", NoOrder));
            cmd.Parameters.Add(new SqlParameter("Id Game", Idgm));
            cmd.Parameters.Add(new SqlParameter("Tanggal", Tgl));
            cmd.Parameters.Add(new SqlParameter("Id pembeli", ip));
            cmd.Parameters.Add(new SqlParameter("Id penjual", idp));
            cmd.Parameters.Add(new SqlParameter("Id admin", ia));

            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil Ditambahkan");
        }
    }
}

