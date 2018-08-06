using LocalizaAmigos.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LocalizaAmigos.DAO
{
    class AmigosDAO
    {
        BaseLocalizarAmigos db;
        public AmigosDAO()
        {
            db = new BaseLocalizarAmigos();
        }

        public List<Amigo> ConsultarAmigos()
        {
            try
            {
                return db.Amigos.ToList();
            }
            catch (Exception)
            {

                throw;
            }                      
        }

        public void InserirAmigo(Amigo _amigo)
        {
            try
            {
                db.Amigos.Add(_amigo);
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Amigo> ConsultarAmigoPorNome(string _nome)
        {
            try
            {
                return db.Amigos.Where(x => x.nome.Contains(_nome)).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Amigo ConsultarAmigoPorID(int _idAmigo)
        {
            try
            {
                return db.Amigos.Where(x => x.amigoID == _idAmigo).First();
            }
            catch (Exception)
            {

                throw;
            }            
        }

        public void AlterarAmigo(Amigo _amigo)
        {
            try
            {
                Amigo amigo;
                amigo = db.Amigos.Where(x => x.amigoID == _amigo.amigoID).First();
                amigo = _amigo;

                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }           
        }
        public void ExcluirAmigo(Amigo _amigo)
        {
            try
            {
                db.Amigos.Remove(_amigo);
                db.SaveChanges();             
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
