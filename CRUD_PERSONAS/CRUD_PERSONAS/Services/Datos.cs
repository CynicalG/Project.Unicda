using CRUD_PERSONAS.Data;
using CRUD_PERSONAS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CRUD_PERSONAS.Services
{
    public class Datos
    {
        private readonly string _connectionString = "Server=localhost;Database=CRUD_PERSONAS;Integrated Security=True;TrustServerCertificate=True;";

        private AppDbContext GetDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(_connectionString);
            return new AppDbContext(optionsBuilder.Options);
        }

        public void Create(Persona p)
        {
            try
            {
                using var db = GetDbContext();
                db.Personas.Add(p);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        public List<Persona> Read()
        {
            try
            {
                using var db = GetDbContext();
                return db.Personas.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al leer: " + ex.Message);
                return new List<Persona>();
            }
        }

        public void Update(Persona p)
        {
            try
            {
                using var db = GetDbContext();
                db.Entry(p).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                using var db = GetDbContext();
                var persona = db.Personas.Find(id);
                if (persona != null)
                {
                    db.Personas.Remove(persona);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
        }
    }
}