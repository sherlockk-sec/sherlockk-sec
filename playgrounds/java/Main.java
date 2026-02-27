import java.util.Scanner;

public class Main {

    static void myMethod(Scanner userInput) {  // Pass Scanner as a parameter
        System.out.println("Enter your first name: ");
        String fname = userInput.nextLine();
        System.out.println(fname + " Akilan");
        
    }

    public static void main(String[] args) {
    
        Scanner userInput = new Scanner(System.in);  // Open Scanner in main
        myMethod(userInput);
        userInput.close();  // Close Scanner after use
        
    }
}