package domain;

import static org.junit.Assert.assertEquals;

import org.junit.Test;


public class CustomerTest {
	@Test
	public void formatThePhoneNumber() throws Exception {
		Customer customer = new Customer("Alberto", "Paez", "54115678654");

		String formattedPhone = customer.formatPhone();

		assertEquals("CountryCode:54 - Citycode:11 - LocalNumber:5678654", formattedPhone);
	}
}
