# MovieMate
This is a video hosting site built in ASP.NET MVC. 
Videos go in Content/master/Movies by default, and due to browser restrictions only mp4 format will work. 
The videos must have an accompanying .json file, which goes in Content/master/Info by default. 
Provided is a template file, and these must be the same filename as the video with extension .json (i.e. Holidays.mp4 will have 
Holidays.json)
You must specify the name and description parameters, others being optional but reccomended to have (there is also a parameter 
'thumbnail' which points to a file which will be used as the thumbnail of the video)
The above paths may be changed in Content/config.json file.
