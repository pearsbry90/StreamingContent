using StreamingContentRepo;
public class ProgramUI{

//Time to Instantiate
private readonly StreamingContentRepository _streamingRepo = new StreamingContentRepository();

public void Run(){
    SeedContentList();
    RunMenu();
}
    public void RunMenu(){
        bool continueToRun = true;
        while(continueToRun){
            Console.Clear();

        Console.WriteLine("Enter the number of the option you'd like to select:\n"+
        "1. Show all streaming content\n"+
        "2. Find streaming content by Title\n"+
        "3. Add new streaming content\n"+
        "4. Remove streaming content\n"+
        "5. Exit");

        string userInput = Console.ReadLine();

        switch(userInput){
            case "1":
            ShowAllContent();
            break;
            case "2":
            ShowContentByTitle();
            break;
            case "3":
            CreateNewContent();
            break;
            case "4":
            RemoveContentFromList();
            break;
            case "5":
            continueToRun = false;
            break;
            default:
            Console.WriteLine("Please Enter a Valid Number between 1 and 5. \n"+
            "Press any key to continue...................");
            Console.ReadKey();
            break;
        }
        }
    }

private void CreateNewContent(){
    Console.Clear(); //Clearing the Menu

    StreamingContent content = new StreamingContent();

    Console.WriteLine("Please enter a Title:");
    content.Title = Console.ReadLine();

    Console.WriteLine("Please enter a brief description of your movie:");
    content.Description = Console.ReadLine();

    Console.WriteLine("Please give us a start rating on your movie:");
    content.StarRating = double.Parse(Console.ReadLine());

    Console.WriteLine("Select a Maturity Rating:\n"+
    "1. G\n"+
    "2. PG\n"+
    "3. PG 13\n"+ 
    "4. R\n"+
    "5. NC 17"+
    "6. TV Y\n"+
    "7. TV G\n"+
    "8. TV PG\n"+
    "9. TV 14\n"+
    "10. TV MA");
    string maturityRating = Console.ReadLine();

    switch(maturityRating){
        case "1":
        content.MaturityRating = "G";
        break;
        case "2":
        content.MaturityRating = "PG";
        break;
        case "3":
        content.MaturityRating = "PG 13";
        break;
        case "4":
        content.MaturityRating = "R";
        break;
        case "5":
        content.MaturityRating = "NC";
        break;
        case "6":
        content.MaturityRating = "TV Y";
        break;
        case "7":
        content.MaturityRating = "TV G";
        break;
        case "8":
        content.MaturityRating = "TV PG";
        break;
        case "9":
        content.MaturityRating = "TV 14";
        break;
         case "10":
        content.MaturityRating = "TV MA";
        break;
    }

    Console.WriteLine("Select a Genre:\n"+
    "1. Horror\n"+
    "2. Comedy\n"+
    "3. Drama\n"+
    "4. Gore\n"+
    "5. Mystery\n"+
    "6. Action");

    string genreInput = Console.ReadLine();
    int genreID = int.Parse(genreInput);
    content.TypeOfGenre = (GenreType)genreID;
    _streamingRepo.AddContentToDirectory(content);
}

private void ShowAllContent(){
    Console.Clear();

    List<StreamingContent> listOfContent = _streamingRepo.GetContents();

    foreach(StreamingContent content in listOfContent){
        DisplayContent(content);
    }
    Console.WriteLine("Press any key to Continue............");
    Console.ReadKey();
}

private void DisplayContent(StreamingContent content){
    Console.WriteLine($"Title: {content.Title}\n"+
    $"Description: {content.Description}\n"+
    $"Genre: {content.TypeOfGenre}\n"+
    $"Star: {content.StarRating}\n"+
    $"Family Friendly: {content.IsFamilyFriendly}\n"+
    $"Maturity Rating: {content.MaturityRating}\n");
}

private void ShowContentByTitle(){
    Console.Clear();

    Console.WriteLine("Enter a Title:");
    
    string title = Console.ReadLine();

    StreamingContent content = _streamingRepo.GetContentByTitle(title);

    if(content != null){
        DisplayContent(content);
    }
    else{
        Console.WriteLine("Invalid Title! Could Not Find Any Results!");
    }

    Console.WriteLine("Press any key to Continue..............");
    Console.ReadKey();
}

private void RemoveContentFromList(){
    Console.Clear();

    Console.WriteLine("Which item would you like to remove?");

    List<StreamingContent> contentList = _streamingRepo.GetContents();

    int count = 0;

    foreach(StreamingContent content in contentList){
        count++;
        Console.WriteLine($"{count}. {content.Title}");
    }

int targetContentID = int.Parse(Console.ReadLine());
int targetIndex = targetContentID - 1;

if(targetIndex >= 0 && targetIndex < contentList.Count){
        StreamingContent desiredContent = contentList[targetIndex];

        if(_streamingRepo.DeleteExistingContent(desiredContent)){
            Console.WriteLine($"{desiredContent.Title} was successfully removed.");
        }        
        else{
            Console.WriteLine("I'm sorry, Dave. I'm afraid I can't do that.");
        }
    Console.WriteLine("Press any key to Continue..........");
    Console.ReadKey();
}
}

private void SeedContentList(){
    // True/False is = Family Friendly?
    StreamingContent rubber = new StreamingContent("rubber", "Tire comes to life and kills people", "R", 5, false);
    StreamingContent toystory = new StreamingContent("Toy Story", "Best Childhood Move", "PG", 5, true);
    StreamingContent starwars = new StreamingContent("Star Wars", "Lightsabers is all you need to know", "PG 13", 5, true);

    _streamingRepo.AddContentToDirectory(rubber);
    _streamingRepo.AddContentToDirectory(toystory);
    _streamingRepo.AddContentToDirectory(starwars);

}
}
