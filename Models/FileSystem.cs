using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using System.Web.Hosting;

namespace MovieMate.Models
{
    public class FileSystem
    {
        internal struct StartupData
        {
            public string movieDirectory;
            public string filmDirectory;
            public string infoDirectory;
        }

        StartupData dat;

        public static string movieDirectory;
        public static string filmDirectory;
        public static string infoDirectory;

        public static List<MovieData> movies = new List<MovieData>();



        public FileSystem()
        {
            dat = JsonConvert.DeserializeObject<StartupData>(File.ReadAllText(HostingEnvironment.MapPath("~/Content/config.json")));



            movieDirectory = dat.movieDirectory;
            filmDirectory = dat.filmDirectory;
            infoDirectory = dat.infoDirectory;
        }

        public void StartFileSystem()
        {

            string[] movieFiles = Directory.GetFiles(HostingEnvironment.MapPath( filmDirectory));
            for(int i = 0; i < movieFiles.Length; i++)
            {
                if (IsMedia(movieFiles[i]))
                {
                    MovieData m = JsonConvert.DeserializeObject<MovieData>(File.ReadAllText(HostingEnvironment.MapPath(infoDirectory) + "/" + FileName(movieFiles[i]) + ".json"));
                    m.filePath = filmDirectory + "/" + FileNameExtension(movieFiles[i]);
                    m.ID = i.ToString();
                    movies.Add(m);
                }
            }

        }

        public static MovieData[] SearchMovies(string input)
        {
            MovieData[] tempd = movies.ToArray();
            List<MovieData> match = new List<MovieData>();
            foreach(MovieData dat in tempd)
            {
                if (dat.name.ToLower().Contains(input.ToLower()))
                    match.Add(dat);
            }
            return match.ToArray();
        }

        public static string ListMoviesHtml()
        {
            StringBuilder s = new StringBuilder();
            MovieData[] tempd = movies.ToArray();
            for (int i = 0; i < tempd.Length; i++)
            {
                //starting crap. jumbotron, icon, etc.
                s.Append("<div class=\"jumbotron-mini row\">\n<div>\n<img src=\"" + tempd[i].thumbnail +"\" class=\"img-film-cover\" />\n" +
                    "</div>\n<div class=\"col-md\" style=\"float:left\">\n");
                //The heading/title
                s.Append("<h3>" + tempd[i].name + "</h3>\n");
                //The description
                s.Append("<p>" + tempd[i].description + "</p>\n");
                //finishing the divs
                s.Append("</div>\n");
                //add de buttons
                s.Append("<div class=\"col-md\"><button class=\"" +
                    "btn btn-success my-2 my-sm-0\" type=\"button\" onclick=\"window.location='");
                s.Append("/Movie/Watch/" + tempd[i].ID);
                s.Append("'\">Watch</button></div>");
                s.Append("</div>");
            }

            return s.ToString();
        }

        public static string ListMoviesHtml(string search)
        {
            MovieData[] tempd = movies.ToArray();

            if (search == null)
                search = "";
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < tempd.Length; i++)
            {
                if (tempd[i].name.ToLower().Contains(search.ToLower()) || search == "")
                {
                    //starting crap. jumbotron, icon, etc.
                    s.Append("<div class=\"jumbotron-mini row\">\n<div>\n<img src=\"" + tempd[i].thumbnail + "\" class=\"img-film-cover\" />\n" +
                        "</div>\n<div class=\"col-md\" style=\"float:left\">\n");
                    //The heading/title
                    s.Append("<h3>" + tempd[i].name + "</h3>\n");
                    //The description
                    s.Append("<p>" + tempd[i].description + "</p>\n");
                    //finishing the divs
                    s.Append("</div>\n");
                    //add de buttons
                    s.Append("<div class=\"col-md\"><button class=\"" +
                        "btn btn-success my-2 my-sm-0\" type=\"button\" onclick=\"window.location='");
                    s.Append("/Movie/Watch/" + tempd[i].ID);
                    s.Append("'\">Watch</button></div>");
                    s.Append("</div>");
                }
            }

            return s.ToString();
        }

        public static string GetMovieSrc(string id)
        {
            MovieData[] tempd = movies.ToArray();
            for (int i = 0; i < tempd.Length; i++)
            {
                if (tempd[i].ID == id)
                    return tempd[i].filePath;
            }
            return id;
        }

        private bool IsMedia(string filename)
        {
            string[] slice = filename.Split('.');
            string fileType = slice[slice.Length - 1];
            switch (fileType)
            {
                case "mp4":
                    return true;
                case "webm":
                    return true;
                case "mkv":
                    return true;

            }
            return false;
        }

        private string FileName(string filename)
        {
            string[] slice = filename.Split('\\');
            string file = slice[slice.Length - 1];

            string file2 = file.Split('.')[0];
            return file2;
        }

        private string FileNameExtension(string filename)
        {
            string[] slice = filename.Split('\\');
            string file = slice[slice.Length - 1];
            return file;
        }
    }

    public class MovieData
    {
        public string filePath;

        public string name;
        public string director;
        public string releaseYear;
        public string description;
        public string thumbnail;
        public string ID;
    }
    
}