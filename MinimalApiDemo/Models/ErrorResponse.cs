// returns an error to customers when there is a problem

namespace MinimalApiDemo.Models
{
       public class ErrorResponse
 {
     public string Title { get; set; }
     public int StatusCode { get; set; }
     public string Message { get; set; }
 }
}
