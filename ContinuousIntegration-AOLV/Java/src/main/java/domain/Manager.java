package domain;

public class Manager extends Employee {
	
	public Manager(String firstName, String lastName, float fixedSalary)
	{
		super(firstName, lastName, fixedSalary);
	}

	/**
	 * @return the salary with additions and deductions
	 */
	public float calculateSalaryAfterAdditionsAndDeductions() {
		float addicionalBenefits = calculateAddicionalBenefits();
		float pensionFounds = this.fixedSalary * 10 / 100;
		float tax = 0;
		if (fixedSalary > 3500)
			tax = fixedSalary * 5 / 100;
		return addicionalBenefits + fixedSalary - pensionFounds - tax;
	}

	private float calculateAddicionalBenefits() {
		return this.subordinates.size() * 20;
	}
}
