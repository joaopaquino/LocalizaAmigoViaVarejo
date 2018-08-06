using System;
using System.Collections.Generic;
using LocalizaAmigos.BO;
using LocalizaAmigos.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LocalizaAmigosTest
{
    [TestClass]
    public class AmigosBOTest
    {

        [TestMethod]
        public void ConsultarAmigosTest()
        {
            AmigosBO amigosBO = new AmigosBO();
            amigosBO.ConsultarAmigos();

            Assert.IsNotNull(amigosBO.ConsultarAmigos());
        }
        [TestMethod]
        public void InserirAmigoTest()
        {
            AmigosBO amigosBO = new AmigosBO();            
            Assert.AreEqual(true, amigosBO.InserirAmigo("TesteUnitario3", 4566, 8736));
        }
        [TestMethod]
        public void InserirAmigoComMesmaLocalizacaoTest()
        {
            AmigosBO amigosBO = new AmigosBO();
            Amigo amigo = amigosBO.ConsultarAmigos()[0];
            Assert.AreEqual(false, amigosBO.InserirAmigo(amigo.nome, amigo.latitude, amigo.longitude));
        }

        [TestMethod]
        public void ConsultarAmigoPorNomeTest()
        {
            AmigosBO amigosBO = new AmigosBO();
            List<Amigo> amigos = amigosBO.ConsultarAmigoPorNome("Teste");

            Assert.IsNotNull(amigos);
        }
        [TestMethod]
        public void AlterarAmigoTest()
        {
            AmigosBO amigosBO = new AmigosBO();
            Amigo amigo = amigosBO.ConsultarAmigos()[0];

            if (amigo != null)
            {
                Assert.AreEqual(true, amigosBO.AlterarAmigo(amigo));
            }
            else
            {
                Assert.Fail("Não exitem carga para teste");
            }
        }
        
        [TestMethod]
        public void BuscarAmigosPorLocalizacaoTest()
        {
            AmigosBO amigosBO = new AmigosBO();
            List<Amigo> amigo = amigosBO.BuscarAmigosPorLocalizacao(123, 123);
            Assert.IsNotNull(amigo);
        }
        [TestMethod]
        public void BuscarAmigosProximos()
        {
            AmigosBO amigosBO = new AmigosBO();
            List<Amigo> amigos = amigosBO.ConsultarAmigos();
            if (amigos != null && amigos.Count > 0)
            {
                Amigo amigo = amigosBO.ConsultarAmigos()[0];
                List<Amigo> amigosProximos = amigosBO.BuscarAmigosProximos(amigo);

                Assert.IsTrue(amigosProximos != null || amigosProximos.Count < 4);
            }            
        }
        [TestMethod]
        public void ExcluirAmigoTest()
        {
            AmigosBO amigosBO = new AmigosBO();
            Amigo amigo = amigosBO.ConsultarAmigos()[0];

            if (amigo != null)
            {
                Assert.AreEqual(true, amigosBO.ExcluirAmigo(amigo.amigoID));
            }
            else
            {
                Assert.Fail("Não exitem carga para teste");
            }
        }
    }
}
