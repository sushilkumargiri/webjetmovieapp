using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebjetMovieAppBL;
using Castle.Windsor;
using WebjetMovieApp.Installers;

namespace WebjetMovieApp.Tests
{
    /// <summary>
    /// Summary description for MovieTest
    /// </summary>
    [TestClass]
    public class MovieTest
    {

        private IWindsorContainer container;
        public MovieTest()
        {
            container = new WindsorContainer()
                        .Install(new ControllersInstaller());
        }

        [TestMethod]
        public void Test_GetCheapestMovie_Success()
        {
            var mock = new Mock<IMovie>();

            List<MovieDetail> data = new List<MovieDetail>();
            List<MovieDetail> movies = new List<MovieDetail>(){
                new MovieDetail() { Title = "Inception", Type = "Movie", ID = "xyzabc1", Year = "2015",Price="100" },
                new MovieDetail() { Title = "Inception 2", Type = "Movie", ID = "xyzabc2", Year = "2016",Price="1" }
            };
            mock.Setup(m => m.GetCheapestMovies(movies, 1)).Returns(() => data);
            var resultList = container.Resolve<IMovie>().GetCheapestMovies(movies, 1);
            Assert.AreEqual(resultList[0].Price, "1");
        }
        [TestMethod]
        public void Test_GetTop2CheapestMovie_Success()
        {
            var mock = new Mock<IMovie>();

            List<MovieDetail> data = new List<MovieDetail>();
            List<MovieDetail> movies = new List<MovieDetail>(){
                new MovieDetail() { Title = "Inception 1", Type = "Movie", ID = "xyzabc1", Year = "2015",Price="100" },
                new MovieDetail() { Title = "Inception 2", Type = "Movie", ID = "xyzabc2", Year = "2015",Price="1" },
                new MovieDetail() { Title = "Inception 3", Type = "Movie", ID = "xyzabc3", Year = "2015",Price="10" }
            };
            mock.Setup(m => m.GetCheapestMovies(movies, 2)).Returns(() => data);
            var resultList = container.Resolve<IMovie>().GetCheapestMovies(movies, 2);
            Assert.AreEqual(resultList[0].Price, "1");
            Assert.AreEqual(resultList[1].Price, "10");
        }
    }
}
