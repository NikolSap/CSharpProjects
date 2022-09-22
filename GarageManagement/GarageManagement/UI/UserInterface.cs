using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExO3.GarageLogic;

namespace UserInterface
{
    public class UserInterface
    {
        private  List<String> s_TryAgainMenuArray =
            new List<string>() { "Try enter another value.", "Back to main menu." };
        public   Garage m_Garage = new Garage();
        public  StringBuilder BuildMenu<T>(ICollection<T> i_Collection)
        {
            int iteratorForPrinting = 0;
            StringBuilder menuStringBuilder = new StringBuilder("Please chose between this options: ");

            foreach (T needToPrint in i_Collection)
            {
                iteratorForPrinting++;
                menuStringBuilder.AppendFormat("    {0}. {1}", iteratorForPrinting, needToPrint.ToString());
            }

            return menuStringBuilder;
        }

        public  int PrintStartingMenuAndGetUserChoice()
        {
            //List<string> userChoiceList = Enum.GetValues(typeof(eUserChoice)).Cast<string>().ToList();
            Dictionary<int, string> startingMenuDictionary = Factory.EnumNamedValues< eUserChoice>();

            return GetValidValueFromEnumDictionary(startingMenuDictionary);
        }

        public  Dictionary<int, string> ConvertListToDictionary(List<string> i_List)
        {
            Dictionary<int, string> listToDictionary = new Dictionary<int, string>();
            int index = 1;

            foreach (string stringFromList in i_List)
            {
                listToDictionary.Add(index, stringFromList);
                index++;
            }

            return listToDictionary;
        }

        public  void StartGarageSystem()
        {
            Console.WriteLine("Welcome to the garage.");
            int userAnswer = PrintStartingMenuAndGetUserChoice();
            eUserChoice userChoice = (eUserChoice)userAnswer;

            while (userChoice != eUserChoice.Exit)
            {
                try
                {
                    switch (userChoice)
                    {
                        case eUserChoice.InsertNewVehicle:
                            {
                                insertVehicleToGarage();
                                break;
                            }
                        case eUserChoice.ShowLicenseNumberOfVehiclesInGarage:
                            {
                                showLicenseNumberOfVehiclesInGarage();
                                break;
                            }
                        case eUserChoice.ChangeVehicleStatusInGarage:
                            {
                                changeVehicleStatusInGarage();
                                break;
                            }
                        case eUserChoice.InflatingWheels:
                            {
                                inflatingWheelsInVehicle();
                                break;
                            }
                        case eUserChoice.RefuelGasolineVehicle:
                            {
                                refuelGasolineVehicle();
                                break;
                            }
                        case eUserChoice.ChargeElectricVehicle:
                            {
                                chargeElectricVehicle();
                                break;
                            }
                        case eUserChoice.ShowVehicleData:
                            {
                                PrintVehicleInGarageData();
                                break;
                            }
                    }
                }
                catch (ValueOutOfRangeException voore)
                {
                    Console.WriteLine(voore);
                }
                catch (FormatException fe)
                {
                    Console.WriteLine(fe.Message);
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    userAnswer = PrintStartingMenuAndGetUserChoice();
                    userChoice = (eUserChoice)userAnswer;
                }
            }

            Console.WriteLine("Thank you for using our garage. Goodbye!");
        }

        public  int GetValidValueFromEnumDictionary(Dictionary<int, string> i_DictionaryOfEnum)
        {
            bool isValid;
            string userAnswere;
            int userChoice;
            StringBuilder optionMenu = new StringBuilder();

            foreach (KeyValuePair<int, string> keyValuePair in i_DictionaryOfEnum)
            {
                if (keyValuePair.Key == 0)
                {
                    optionMenu.Append(keyValuePair.Value);
                    optionMenu.AppendLine();
                }
                else
                {
                    optionMenu.AppendFormat("{0} - {1}", keyValuePair.Key, keyValuePair.Value);
                    optionMenu.AppendLine();
                }
            }

            Console.WriteLine(optionMenu);

            do
            {
                userAnswere = Console.ReadLine();
                isValid = int.TryParse(userAnswere, out userChoice) && i_DictionaryOfEnum.ContainsKey(userChoice);

                if(!isValid)
                {
                    Console.WriteLine("Please enter a valid choice.");
                }
            }
            while(!isValid);

            return userChoice;
        }

        private  void insertVehicleToGarage()
        {
            Dictionary<string, object> classMembers = new Dictionary<string, object>();
            Dictionary<string, object> objectToConstructor = new Dictionary<string, object>();
            Dictionary<string, int> enumData = new Dictionary<string, int>();
            string licenseNumber = getValidLicenseNumber();
            Garage.VehicleInGarage vehicleInGarage = getVehicleInGarageByLicenseNumber(licenseNumber);
            Vehicle vehicleToAdd;
            int vehicleType;

            if (vehicleInGarage == null)
            {
                vehicleType = GetValidValueFromEnumDictionary(Factory.GetVehicleTypesDictionary());
                classMembers = Factory.GetClassDictionary(vehicleType);
                Dictionary<string, Dictionary<int, string>> collectionOfEnums = Factory.GetEnumDictionary(vehicleType);

                getClassMembersData(objectToConstructor, classMembers);
                getEnumsChoices(enumData, collectionOfEnums);
                objectToConstructor["license number"] = licenseNumber;

                try
                {
                    vehicleToAdd = Factory.BuildTheVehicle(vehicleType, objectToConstructor, enumData);
                    objectToConstructor.Clear();
                    classMembers = Garage.VehicleInGarage.GetDictionaryOfTheClass();
                    getCustomerData(objectToConstructor, classMembers);
                    vehicleInGarage = m_Garage.BuildTheVehicleInGarage(vehicleToAdd, objectToConstructor);
                    m_Garage.vehiclesInGarage.Add(vehicleInGarage.Vehicle.LicenseNumber, vehicleInGarage);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            else
            {
                Console.WriteLine("Your vehicle already exists in the garage. The status has been change to 'in treatment'");
                vehicleInGarage.FixingStatus = Garage.VehicleInGarage.eVehicleFixingStatus.InTreatment;
            }
        }

        private  void getClassMembersData(Dictionary<string, object> io_ObjectToConstructor, Dictionary<string, object> i_ClassMembers)
        {
            foreach (KeyValuePair<string, object> nameAndTypeOfClassMember in i_ClassMembers)
            {
                if(nameAndTypeOfClassMember.Key != "license number")
                {
                    try
                    {
                        io_ObjectToConstructor.Add(nameAndTypeOfClassMember.Key, tryToGetTheObjectValue(nameAndTypeOfClassMember));

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
            }

        }

        private  void getEnumsChoices(Dictionary<string, int> io_ObjectToConstructor, Dictionary<string, Dictionary<int, string>> i_EnumMembers)
        {
            foreach (KeyValuePair<string, Dictionary<int, string>> classEnumMember in i_EnumMembers)
            {
                try
                {
                    io_ObjectToConstructor.Add(classEnumMember.Key, GetValidValueFromEnumDictionary(classEnumMember.Value));

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        private  void getCustomerData(Dictionary<string, object> io_ObjectToConstructor, Dictionary<string, object> i_ClassMembers)
        {
            foreach (KeyValuePair<string, object> nameAndTypeOfClassMember in i_ClassMembers)
            {
                try
                {
                    io_ObjectToConstructor.Add(nameAndTypeOfClassMember.Key, tryToGetTheObjectValue(nameAndTypeOfClassMember));

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        private  object tryToGetTheObjectValue(KeyValuePair<string, object> i_NameAndTypeOfClassMember)
        {
            object obj = i_NameAndTypeOfClassMember.Value;
            //string obj = typeof(abject);
            Console.WriteLine("Please enter the {0} value of the vehicle: ", i_NameAndTypeOfClassMember.Key);
            object generalValue = new object();
            Type typofobj = GetType();
            if ((ReferenceEquals(obj,typeof(int))))
            {
                generalValue = getValidIntValue();
            }
            else if (ReferenceEquals(obj, typeof(float)))
            {
                generalValue = getValidFloatValue();
            }
            else if (ReferenceEquals(obj, typeof(bool)))
            {
                generalValue = getValidBoolValue(i_NameAndTypeOfClassMember.Key);
            }
            else if (ReferenceEquals(obj, typeof(string))/*i_NameAndTypeOfClassMember.Value is string*/)
            {

                generalValue = Console.ReadLine();
            }
            else if (ReferenceEquals(obj, typeof(double)))
            {
                generalValue = getValidDoubleValue();

            }
            else if (ReferenceEquals(obj, typeof(char)))
            {
                generalValue = getValidCharValue();

            }
            else if (ReferenceEquals(obj, typeof(Wheel)))
            {
                generalValue = getValidWheel();
            }
            else
            {
                throw new ArgumentException("This object type doesn't supported in this program.");
            }
            
            return generalValue;
        }

        private  void showLicenseNumberOfVehiclesInGarage()
        {
            List<Garage.VehicleInGarage.eVehicleFixingStatus> listOfStatus;
            int vehicleStatus = getChoiceIfFilterVehiclesByStatus();
            try
            {
                if (vehicleStatus == 2)
                {
                    listOfStatus = Enum.GetValues(typeof(Garage.VehicleInGarage.eVehicleFixingStatus))
                        .Cast<Garage.VehicleInGarage.eVehicleFixingStatus>().ToList();
                    printLicenseNumberOfVehiclesInGarage(listOfStatus);
                }
                else
                {
                    listOfStatus = getVehicleStatusForFiltering();
                    printLicenseNumberOfVehiclesInGarage(listOfStatus);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        private  int getChoiceIfFilterVehiclesByStatus()
        {
            int userChoice;
            StringBuilder filterVehicleStatusString = new StringBuilder();

            filterVehicleStatusString.Append("Please enter your choice:");
            filterVehicleStatusString.AppendLine();
            Console.WriteLine(filterVehicleStatusString);
            Dictionary<int, string> isUserWantToFilterVehicleStatus = new Dictionary<int, string>();
            isUserWantToFilterVehicleStatus.Add(1, "Filer vehicles by their status");
            isUserWantToFilterVehicleStatus.Add(2, " Not filter vehicles by their status");
            userChoice = GetValidValueFromEnumDictionary(isUserWantToFilterVehicleStatus);

            return userChoice;
        }

        private  List<Garage.VehicleInGarage.eVehicleFixingStatus> getVehicleStatusForFiltering()
        {
            List<Garage.VehicleInGarage.eVehicleFixingStatus> filterStatusList = new List<Garage.VehicleInGarage.eVehicleFixingStatus>();
            int validVehicleStatus;
            int additionalStatusFilter;
            bool userDone = false;

            Console.WriteLine("Please enter vehicle status for filtering: " + Environment.NewLine);

            while (!userDone)
            {
                validVehicleStatus = getValidVehicleFixingStatus(filterStatusList);
                filterStatusList.Add((Garage.VehicleInGarage.eVehicleFixingStatus)validVehicleStatus);
                additionalStatusFilter = getValidChoiceFromUserForAdditionalFilter();
                userDone = additionalStatusFilter == 2;
            }

            Console.Clear();

            return filterStatusList;
        }

        private  int getValidVehicleFixingStatus(List<Garage.VehicleInGarage.eVehicleFixingStatus> i_FilterStatusList)
        {
            printVehicleFixingStatus();
            int.TryParse(Console.ReadLine(), out int vehicleStatusChoice);

            while (i_FilterStatusList.Contains((Garage.VehicleInGarage.eVehicleFixingStatus)vehicleStatusChoice)
                  || vehicleStatusChoice != 0 && vehicleStatusChoice != 1 && vehicleStatusChoice != 2
                  && vehicleStatusChoice != 3)
            {

                if (i_FilterStatusList.Contains((Garage.VehicleInGarage.eVehicleFixingStatus)vehicleStatusChoice))
                {
                    Console.WriteLine(
                        "This status already chosen. Please enter a new status: " + Environment.NewLine);
                }
                else
                {
                    Console.WriteLine("Please enter a valid vehicle status:" + Environment.NewLine);
                }

                int.TryParse(Console.ReadLine(), out vehicleStatusChoice);
            }

            return vehicleStatusChoice;
        }

        private  int getValidChoiceFromUserForAdditionalFilter()
        {
            Console.WriteLine("Press 1 for additional status filter");
            Console.WriteLine("Otherwise press 2 ");
            int.TryParse(Console.ReadLine(), out int additionalStatusFilter);


            while (additionalStatusFilter != 1 && additionalStatusFilter != 2)
            {
                Console.WriteLine("Please enter a valid choice:" + Environment.NewLine);
                int.TryParse(Console.ReadLine(), out additionalStatusFilter);
            }

            return additionalStatusFilter;
        }

        private  void printLicenseNumberOfVehiclesInGarage(List<Garage.VehicleInGarage.eVehicleFixingStatus> i_FilterStatusList)
        {
            Console.WriteLine("The license Number of the vehicles are:");

            foreach (Garage.VehicleInGarage vehicleInGarage in m_Garage.vehiclesInGarage.Values)
            {
                int numberOfParams = i_FilterStatusList.Count;
                numberOfParams--;

                while (numberOfParams >= 0)
                {

                    if (vehicleInGarage.FixingStatus == i_FilterStatusList[numberOfParams])
                    {
                        Console.WriteLine(vehicleInGarage.Vehicle.LicenseNumber);
                    }

                    numberOfParams--;
                }

            }

        }

        private  void changeVehicleStatusInGarage()
        {
            string licenseNumber = getValidLicenseNumber();
            Garage.VehicleInGarage vehicleInGarage = getVehicleInGarageByLicenseNumber(licenseNumber);

            if (vehicleInGarage == null)
            {
                throw new ArgumentException("This license number don't exist in the garage.");
            }

            Garage.VehicleInGarage.eVehicleFixingStatus newStatus = getValidNewStatus();
            vehicleInGarage.FixingStatus = newStatus;
            Console.WriteLine("The new status of vehicle {0} is: {1}", licenseNumber, vehicleInGarage.FixingStatus);
        }

        private  void printVehicleFixingStatus()
        {
            Dictionary<int, string> vehicleFixingStatusIDictionary =
                Factory.EnumNamedValues<Garage.VehicleInGarage.eVehicleFixingStatus>();

            foreach (KeyValuePair<int, string> fixingStatus in vehicleFixingStatusIDictionary)
            {
                Console.WriteLine("{0} - {1}", fixingStatus.Key, fixingStatus.Value);
            }

        }

        private  Garage.VehicleInGarage.eVehicleFixingStatus getValidNewStatus()
        {
            bool isValid;

            Console.WriteLine("Please enter new vehicle status: ");
            printVehicleFixingStatus();
            isValid = Enum.TryParse(Console.ReadLine(), out Garage.VehicleInGarage.eVehicleFixingStatus fixingStatus);

            while (!isValid)
            {
                Console.WriteLine("Please enter a valid status");
                isValid = Enum.TryParse(Console.ReadLine(), out fixingStatus);
            }

            return fixingStatus;
        }

        private  string getValidLicenseNumber()
        {
            Console.Write("Please enter Your license number:" + Environment.NewLine);
            string licenseNumber = Console.ReadLine();

            if (!isLicenseNumberValid(licenseNumber))
            {
                throw new ArgumentException("The license number you entered is invalid.");
            }

            return licenseNumber;
        }

        private  bool isLicenseNumberValid(string i_LicenseNumber)
        {
            bool isLicenseNumberValid = true;

            foreach (char digitInLicenseNumber in i_LicenseNumber)
            {

                if (!char.IsDigit(digitInLicenseNumber))
                {
                    isLicenseNumberValid = false;
                }

            }

            if (i_LicenseNumber.Length != 8)
            {
                isLicenseNumberValid = false;
            }

            return isLicenseNumberValid;
        }

        private  Garage.VehicleInGarage getVehicleInGarageByLicenseNumber(string i_LicenseNumber)
        {

            m_Garage.vehiclesInGarage.TryGetValue(i_LicenseNumber, out Garage.VehicleInGarage vehicleInGarage);
            return vehicleInGarage;

        }

        private  void inflatingWheelsInVehicle()
        {
            string licenseNumber = getValidLicenseNumber();
            Garage.VehicleInGarage vehicleInGarage = getVehicleInGarageByLicenseNumber(licenseNumber);

            if (vehicleInGarage == null)
            {
                throw new ArgumentException("This license number don't exist in the garage.");
            }

            bool inflatingAllWheelsInVehicle = getIfUserWantInflatingAllWheels();

            if (inflatingAllWheelsInVehicle)
            {
                inflatingAllVehicleWheelsToMaximum(vehicleInGarage);
            }
            else
            {
                inflatingPartOfVehicleWheelsToMaximum(vehicleInGarage);
            }

        }

        private  bool getIfUserWantInflatingAllWheels()
        {
            int userChoice;
            Dictionary<int, string> isUserWantToInflatingAllWheelsOrPartOfWheels = new Dictionary<int, string>();

            Console.WriteLine("Please enter your choice:");
            isUserWantToInflatingAllWheelsOrPartOfWheels.Add(1, "Inflating all wheels to maximum air pressure.");
            isUserWantToInflatingAllWheelsOrPartOfWheels.Add(2, " Inflating part of wheels to maximum air pressure.");
            userChoice = GetValidValueFromEnumDictionary(isUserWantToInflatingAllWheelsOrPartOfWheels);

            return userChoice == 1;
        }

        private  void inflatingAllVehicleWheelsToMaximum(Garage.VehicleInGarage i_VehicleInGarage)
        {
            foreach (Wheel wheel in i_VehicleInGarage.Vehicle.WheelCollection)
            {
                wheel.InflatingAWheel(wheel.MaximumAirPressure - wheel.CurrentAirPressure);
            }
        }

        private  void inflatingPartOfVehicleWheelsToMaximum(Garage.VehicleInGarage i_VehicleInGarage)
        {
            int wheelIndex = 1;
            float airPressureToAdd;
            bool isUserDone = false;
            List<string> wheelPlaces = m_Garage.GetWheelsPlaceByVehicleType(i_VehicleInGarage.Vehicle);

            while (!isUserDone)
            {
                Console.WriteLine("Please enter which wheel to inflate:");

                foreach (string wheelPlace in wheelPlaces)
                {
                    Console.WriteLine("{0} - {1} ", wheelIndex, wheelPlace);
                }

                int.TryParse(Console.ReadLine(), out int numberOfWheel);

                if (numberOfWheel < 1 || numberOfWheel > i_VehicleInGarage.Vehicle.WheelCollection.Count)
                {
                    throw new ValueOutOfRangeException(1, i_VehicleInGarage.Vehicle.WheelCollection.Count);
                }

                airPressureToAdd = i_VehicleInGarage.Vehicle.WheelCollection[numberOfWheel - 1].MaximumAirPressure
                                   - i_VehicleInGarage.Vehicle.WheelCollection[numberOfWheel - 1].CurrentAirPressure;

                if (airPressureToAdd == 0)
                {
                    Console.WriteLine("This wheel already in the maximum air pressure.");
                }
                else
                {
                    i_VehicleInGarage.Vehicle.WheelCollection[numberOfWheel - 1].InflatingAWheel(airPressureToAdd);
                }

                Console.WriteLine("If you want to inflate another wheel press 1.");
                Console.WriteLine("Otherwise press 2.");
                int.TryParse(Console.ReadLine(), out int userChoiceToContinue);

                while (userChoiceToContinue != 1 && userChoiceToContinue != 2)
                {
                    Console.WriteLine("Please enter a valid choice.");
                    int.TryParse(Console.ReadLine(), out userChoiceToContinue);
                }

                isUserDone = userChoiceToContinue == 2;
            }
        }

        private  int getValidIntValue()
        {
            if (!int.TryParse(Console.ReadLine(), out int intValue))
            {
                throw new FormatException("Invalid value type for this vehicle property.");
            }

            return intValue;
        }

        private  float getValidFloatValue()
        {
            if (!float.TryParse(Console.ReadLine(), out float floatValue))
            {
                throw new FormatException("Invalid value type for this vehicle property.");
            }

            return floatValue;
        }

        private  bool getValidBoolValue(string i_NameOfClassMember)
        {
            StringBuilder twoOptionsMenu = new StringBuilder();

            twoOptionsMenu.AppendFormat("This vehicle is {0}", i_NameOfClassMember);
            twoOptionsMenu.Append("Press '1' for yes, and '2' for no.");
            Console.WriteLine(twoOptionsMenu);
            int.TryParse(Console.ReadLine(), out int userInput);
            twoOptionsMenu.Clear();

            while (userInput != 1 && userInput != 2)
            {
                twoOptionsMenu.Append("Please enter a valid choice.");
                int.TryParse(Console.ReadLine(), out userInput);
            }

            return userInput == 1;
        }

        private  char getValidCharValue()
        {
            if (!char.TryParse(Console.ReadLine(), out char charValue))
            {
                throw new FormatException("Invalid value type for this vehicle property.");
            }

            return charValue;
        }

        private  double getValidDoubleValue()
        {
            if (!double.TryParse(Console.ReadLine(), out double doubleValue))
            {
                throw new FormatException("Invalid value type for this vehicle property.");
            }

            return doubleValue;
        }

        private  List<object> getValidWheel()
        {
            Dictionary<string, object> wheelDictionary = Wheel.GetDictionaryOfTheClass();
            List<object> objectToConstructor = new List<object>();

            foreach (KeyValuePair<string, object> nameAndTypeOfClassMember in wheelDictionary)
            {
                try
                {
                    objectToConstructor.Add(tryToGetTheObjectValue(nameAndTypeOfClassMember));

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

            }

            return objectToConstructor;
        }

        private  void refuelGasolineVehicle()
        {
            string LicenseNumberFromUser;
            GasolineFuelCell.eGasolineType fuelType;
            float amountOfFuel;

            try
            {
                LicenseNumberFromUser = getLicenseNumber();
                fuelType = getFuelType();
                amountOfFuel = getAmountOfFuel();
                m_Garage.RefuelVehicle(LicenseNumberFromUser, amountOfFuel, fuelType);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        private  void chargeElectricVehicle()
        {
            string licenseNumberFromUser;
            float amountOfHoursToAdd;

            try
            {
                licenseNumberFromUser = getLicenseNumber();
                amountOfHoursToAdd = getHowManyHoursToAdd();
                m_Garage.ChargeVehicle(licenseNumberFromUser, amountOfHoursToAdd);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        private  GasolineFuelCell.eGasolineType getFuelType()
        {
            string userChoice;
            int validAnswere;

            List<GasolineFuelCell.eGasolineType> listOfFuelType = GasolineFuelCell.GetGasolineTypes();
            StringBuilder fuelTypeMenu = new StringBuilder("Please enter the the fuel type you wants: ");
            fuelTypeMenu.Append(BuildMenu(listOfFuelType));
            Console.WriteLine(fuelTypeMenu);
            userChoice = Console.ReadLine();

            if (!int.TryParse(userChoice, out validAnswere))
            {
                throw new FormatException("Not valid input.");
            }

            if (validAnswere < 1 || validAnswere > listOfFuelType.Count)
            {
                throw new ValueOutOfRangeException(1, listOfFuelType.Count);
            }

            return listOfFuelType[validAnswere - 1];
        }

        private  float getHowManyHoursToAdd()
        {
            string userAnswer;

            Console.WriteLine("Please enter the hours that you want to add to battery charge: ");
            userAnswer = Console.ReadLine();

            if (!float.TryParse(userAnswer, out float amountOfHours))
            {
                throw (new FormatException("Not Valid value."));
            }

            return amountOfHours;
        }

        private  float getAmountOfFuel()
        {
            string userAnswer;

            Console.WriteLine("Please enter the amount of fuel to add: ");
            userAnswer = Console.ReadLine();

            if (!float.TryParse(userAnswer, out float amountOfFuel))
            {
                throw (new FormatException("Not Valid value."));
            }

            return amountOfFuel;
        }

        private  string getLicenseNumber()
        {
            string userAnswer;

            Console.WriteLine("Please enter the license number: ");
            userAnswer = Console.ReadLine();

            if (!m_Garage.IsVehicleExists(userAnswer))
            {
                throw new ArgumentException("Vehicle doesn't exists.");
            }

            return userAnswer;
        }

        public  void PrintVehicleInGarageData()
        {
            string userInput = getLicenseNumber();
            Garage.VehicleInGarage vehicle = getVehicleInGarageByLicenseNumber(userInput);
            Console.WriteLine(vehicle.ToString());
        }
    }
}