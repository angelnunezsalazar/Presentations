package domain;

public class Customer extends ThirdParty {

	public String firstName;
	public String lastName;
	
	/**
	 * @param firstName
	 * @param lastName
	 * @param phoneNumber
	 */
	public Customer(String firstName, String lastName, String phoneNumber) {
		super(phoneNumber);
		this.firstName = firstName;
		this.lastName = lastName;
	}
	
	/**
	 * @return first name
	 */
	public String getFirstName() {
		return firstName;
	}

	/**
	 * @return last name
	 */
	public String getLastName() {
		return lastName;
	}
}
