using LocalizaAmigos.DAO;
using LocalizaAmigos.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LocalizaAmigos.BO
{
    public class AmigosBO
    {
        AmigosDAO amigosDAO;

        public AmigosBO()
        {
            amigosDAO = new AmigosDAO();
        }
        public List<Amigo> ConsultarAmigos ()
        {
            return amigosDAO.ConsultarAmigos();       
        }
        public bool InserirAmigo(string _nome, double _latitude, double _longetude)
        {
            List<Amigo> ComparaAmigos = BuscarAmigosPorLocalizacao(_latitude, _longetude);
            if (ComparaAmigos == null || ComparaAmigos.Count == 0)
            {
                Amigo amigo = new Amigo
                {
                    nome = _nome,
                    latitude = _latitude,
                    longitude = _longetude
                };
                amigosDAO.InserirAmigo(amigo);
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<Amigo> ConsultarAmigoPorNome(string _nome)
        {
            return amigosDAO.ConsultarAmigoPorNome(_nome);
        }
        public bool AlterarAmigo(Amigo _amigo)
        {
            List<Amigo> ComparaAmigos = BuscarAmigosPorLocalizacao(_amigo.latitude, _amigo.longitude).Where(x => x.amigoID != _amigo.amigoID).ToList();

            if (ComparaAmigos != null || !(ComparaAmigos.Count > 0))
            {
                amigosDAO.AlterarAmigo(_amigo);
                return true;
            }
            else { return false; }
        }
        public bool ExcluirAmigo(int _idAmigo)
        {
            Amigo amigo = amigosDAO.ConsultarAmigoPorID(_idAmigo);
            if (amigo != null)
            {
                amigosDAO.ExcluirAmigo(amigo);
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<Amigo> BuscarAmigosPorLocalizacao(double _latitude, double _longetude)
        {
            return amigosDAO.ConsultarAmigos().Where(x => x.latitude == _latitude && x.longitude == _longetude).ToList();            
        }

        public List<Amigo> BuscarAmigosProximos(Amigo _amigoVisitado)
        {
            List<Amigo> amigos = amigosDAO.ConsultarAmigos();            
            amigos.Remove(_amigoVisitado);
            if (amigos != null || amigos.Count > 0)
            {
                List<Amigo> amigosProximos = new List<Amigo>();
                foreach (var amigo in amigos)
                {
                    if (amigosProximos.Count < 3)
                    {
                        amigosProximos.Add(amigo);
                    }
                    else
                    {
                        foreach (var amigoProximo in amigosProximos)
                        {
                            double distanciaProximos = CalcularDistanciaAmigo(amigoProximo.latitude, amigoProximo.longitude, _amigoVisitado.latitude, _amigoVisitado.longitude);
                            double distanciaAmigos = CalcularDistanciaAmigo(amigo.latitude, amigo.longitude, _amigoVisitado.latitude, _amigoVisitado.longitude);

                            if (distanciaAmigos < distanciaProximos)
                            {
                                amigosProximos.Add(amigo);
                                if(amigosProximos.Count == 3)
                                amigosProximos.Remove(amigoProximo);
                            }
                        }   
                    }
                }
                return amigosProximos;
            }
            else
            {
                return null;
            }
        }
        double CalcularDistanciaAmigo(double _latAmigoA, double _longAmigoA, double _latAmigoB, double _longAmigoB)
        {
            double calcDistancia;
            double calcLat;
            double calcLong;
            calcLat = Math.Pow((_latAmigoA - _latAmigoB), 2);
            calcLong = Math.Pow(_longAmigoA - _longAmigoB, 2);
            calcDistancia = Math.Sqrt(calcLat + calcLong);

            return calcDistancia;
        }
    }
}
