using CRUD_PEOPLE.Data;
using Microsoft.EntityFrameworkCore;
using CRUD_PEOPLE.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CRUD_PEOPLE.Services
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
                using AppDbContext db = GetDbContext();
                var entry = db.Entry(p) as IDbEntry<Persona>;
                if (entry != null)
                    entry.State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message);
            }
        }

        public AppDbContext GetDb()
        {
            return GetDbContext();
        }

        public void Delete(int id, AppDbContext db)
        {
            try
            {
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