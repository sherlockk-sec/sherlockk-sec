public class w3 {
    public static void main(String[] args) {
        String txt = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        System.out.println("The length of the txt string is: " + txt.length());


        String txt1 = "Hello World";
        System.out.println(txt1.toUpperCase());   // Outputs "HELLO WORLD"
        System.out.println(txt1.toLowerCase());   // Outputs "hello world"
        

        String txt2 = "Please locate where 'locate' occurs!";
        System.out.println(txt2.indexOf("locate")); // Outputs 7

        String firstName = "John ";
        String lastName = "Doe";
        System.out.println(firstName.concat(lastName));
        

        // variable = (condition) ? expressionTrue :  expressionFalse;


        // for (type variableName : arrayName) {
        // code block to be executed


        /*looping through an array using for each
        String[] cars = {"Volvo", "BMW", "Ford", "Mazda"};
        for (String i : cars) {
        System.out.println(i);
        }
        */


    }
}