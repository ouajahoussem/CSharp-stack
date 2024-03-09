// See https://aka.ms/new-console-template for more information

Vehicle car = new Vehicle("ford mustang","red");
Vehicle bicycle = new Vehicle("btwin",1,"green",false);
Vehicle scooter = new Vehicle("gilera",2,"black",true);
Vehicle quad= new Vehicle("yamaha",2,"white",true);

List<Vehicle> allVehicle = new List<Vehicle>();
allVehicle.Add(car);
allVehicle.Add(bicycle);
allVehicle.Add(scooter);
allVehicle.Add(quad);


car.Travel(100);

car.showInfo();

foreach(Vehicle v in allVehicle)
{
    v.showInfo();
}