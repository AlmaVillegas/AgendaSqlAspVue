using System;    
using System.Collections.Generic;    
using System.Data;    
using System.Data.SqlClient;    
using System.Linq;    
using System.Threading.Tasks;

namespace AgendaSqlAspVue.Models
{
    public class EmployeeDataAccessLayer 
    {
        AgendaSqlAspVue.Conexion.Conexion cone= new AgendaSqlAspVue.Conexion.Conexion();
        string connectionString = "Data Source=PROSERVER;Initial Catalog=IntelisisTmp;User ID=arbolanos;Password=Ramona1995";
                                   
        //view all employee details 
        public IEnumerable<Employee> GetAllEmployees(){
            List<Employee> lstemployee = new List<Employee>();

            using (SqlConnection con = cone.getConnection()){
                SqlCommand cmd = new SqlCommand("spGetAllEmployees", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr= cmd.ExecuteReader();
               
                while(rdr.Read()) {
                    Employee employee=new Employee();
                    employee.ID=Convert.ToInt32(rdr["EmployeeID"]);
                    employee.Name=rdr["Name"].ToString();
                    employee.Gender=rdr["Gender"].ToString();
                    employee.Department=rdr["Department"].ToString();
                    employee.City=rdr["City"].ToString();
                    lstemployee.Add(employee);
                }
                con.Close();
            }
            return lstemployee;
        }
        //add new employee
        public void AddEmployee(Employee employee){
            using (SqlConnection con= new SqlConnection(connectionString)){
                
                SqlCommand cmd= new SqlCommand("spAddEmploye", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name",employee.Name);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@Department", employee.Department);
                cmd.Parameters.AddWithValue("@City", employee.City);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public void UpdateEmployee(Employee employee)  
        {  
            using (SqlConnection con = new SqlConnection(connectionString))  
            {  
                SqlCommand cmd = new SqlCommand("spUpdateEmployee", con);  
                cmd.CommandType = CommandType.StoredProcedure;  
  
                cmd.Parameters.AddWithValue("@EmpId", employee.ID);  
                cmd.Parameters.AddWithValue("@Name", employee.Name);  
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);  
                cmd.Parameters.AddWithValue("@Department", employee.Department);  
                cmd.Parameters.AddWithValue("@City", employee.City);  
  
                con.Open();  
                cmd.ExecuteNonQuery();  
                con.Close();  
            }  
        }  
  
        public Employee GetEmployeeData(int? id){

            Employee employee= new Employee();
            using (SqlConnection con = new SqlConnection(connectionString)){
                string sqlQuery="Select * from TblEmployee where EmployeeId= "+id;
                SqlCommand cmd= new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader rdr= cmd.ExecuteReader();
                while(rdr.Read()){
                    employee.ID=Convert.ToInt32(rdr["EmployeeID"]);
                    employee.Name=rdr["Name"].ToString();
                    employee.Gender=rdr["Gender"].ToString();
                    employee.Department=rdr["Department"].ToString();
                    employee.City=rdr["City"].ToString();
                }
            }
            return employee;
        }
        public void DeleteEmployee(int? id){
            using (SqlConnection con = new SqlConnection(connectionString)){
                SqlCommand cmd= new SqlCommand("spDeleteEmployee", con);
                cmd.CommandType= CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        } 
    }
}