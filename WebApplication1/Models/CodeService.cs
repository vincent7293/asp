﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class CodeService
    {
        private string GetDBConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }
        public List<Order> GetCustomerList()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT [CustomerID] as CustId,[CompanyName] as CustName
                            FROM [Sales].[Customers]";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();

            }
            return this.MapCustomerDataToList(dt);
        }
        public List<Order> GetEmpList()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT [EmployeeID] as EmpId,[FirstName]+' '+[LastName] as EmpName FROM [HR].[Employees]";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();

            }
            return this.MapEmpDataToList(dt);

        }
        public List<Order> GetShipperList()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT [ShipperID] as ShipperId ,[CompanyName] as ShipperName FROM [Sales].[Shippers]";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();

            }
            return this.MapShipperDataToList(dt);

        }
        public List<OrderDetail> GetProductList()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT ProductId, ProductName, convert(float, UnitPrice, 1) as UnitPrice FROM [Production].[Products] ";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();

            }
            return this.MapProductDataToList(dt);

        }
        private List<Models.Order> MapCustomerDataToList(DataTable orderData)
        {
            List<Models.Order> result = new List<Order>();

            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new Order()
                {
                    CustId = (int)row["CustId"],
                    CustName = row["CustName"].ToString()
                });
            }
            return result;
        }
        private List<Models.Order> MapEmpDataToList(DataTable orderData)
        {
            List<Models.Order> result = new List<Order>();

            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new Order()
                {
                    EmpId = (int)row["EmpId"],
                    EmpName = row["EmpName"].ToString()
                });
            }
            return result;
        }
        private List<Models.Order> MapShipperDataToList(DataTable orderData)
        {
            List<Models.Order> result = new List<Order>();

            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new Order()
                {
                    ShipperId = (int)row["ShipperId"],
                    ShipperName = row["ShipperName"].ToString()
                });
            }
            return result;
        }
        private List<Models.OrderDetail> MapProductDataToList(DataTable orderData)
        {
            List<Models.OrderDetail> result = new List<OrderDetail>();

            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new OrderDetail()
                {
                    ProductId = (int)row["ProductId"],
                    ProductName = row["ProductName"].ToString(),
                    UnitPrice = (Double)row["UnitPrice"],

                });
            }
            return result;
        }

    }
}