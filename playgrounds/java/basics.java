import java.util.Scanner;
public class basics {
    public static void main(String[] args) {
        
        //this is a comment
        /*

        this is a multi line comment
        */
    System.out.print("hello\n");
       
    int age = 89; 
    System.out.println("The age is "+ age);

    //boolean
    boolean isStudent = true;
    if(isStudent){
        System.out.println("You are a student");

    } else{
        System.out.println("You are a not student");

    }
    // reference data types
    String name = "karthikeyan";
    System.out.println(name);

    //user input
    Scanner userInput = new Scanner(System.in);
    System.out.println("Enter your name: ");
    String name1 = userInput.nextLine();

    System.out.println("Hello " + name1);
    userInput.close();

    String txt = "Please locate where 'locate' occurs!";
    System.out.println(txt.indexOf("zoo"));


    }
}