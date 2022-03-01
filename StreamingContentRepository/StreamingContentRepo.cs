namespace StreamingContentRepo;
public class StreamingContentRepository{
    // An Access Modifier
    protected readonly List<StreamingContent> _contentDirectory = new List<StreamingContent>();
public bool AddContentToDirectory(StreamingContent content){
    int startingCount = _contentDirectory.Count;

    _contentDirectory.Add(content);

    bool wasAdded = (_contentDirectory.Count > startingCount) ? true : false;
    return wasAdded;
}

public List<StreamingContent> GetContents(){
    return _contentDirectory;
}

//ToLower is a method: all yellow words are methods (created by other users)
public StreamingContent GetContentByTitle(string title){
    foreach (StreamingContent content in _contentDirectory){
        // For every information in my list, I'll have to pull out each content in the list
        if(content.Title.ToLower() == title.ToLower()){
            return content;
        }
    }
    return null; //If there's an error searching (title name, spelling, etc) this will return and do nothing (like the 404)
}

public bool UpdateExistingContent(string originalTitle, StreamingContent newContent){
    StreamingContent oldContent = GetContentByTitle(originalTitle);
    
    if(oldContent != null){
        oldContent.Title = newContent.Title;
        oldContent.Description = newContent.Description;
        oldContent.StarRating = newContent.StarRating;
        oldContent.MaturityRating = newContent.MaturityRating;
        oldContent.TypeOfGenre = newContent.TypeOfGenre;

        return true;
    }
    else{
        return false;
    }
}
public bool DeleteExistingContent(StreamingContent existingContent){
    bool deleteResult = _contentDirectory.Remove(existingContent);
    return deleteResult;
}
}