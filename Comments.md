# Backend Coding Exercise 
# Assumptions
1. Current SlothEnterprise.ProductApplication project type is library. 
Assumption is that application will be used as a library 
and the code that use the application will provide external dependencies
(implementations for external interfaces)

#Top 5 Main Concerns

1. ProductApplicationService.SubmitApplicationFor() violates Liskov substitution principle.
The method knows details of IProduct different implementations.  
One way to fix that is to extend IProduct interface with ConvertToCommand() method
and add dispatcher dependency into ProductApplicationService 
so it will be able to run command without knowing exact service that processes the command.
After refactoring adding new products will be much easier, because it can be done without 
changing ProductApplicationService.

2. No test coverage.
Although solution includes Tests project it doesn't provide any tests.

3. Mapping between ISellerCompanyData and CompanyDataRequest and similar relations
can be implemented in separate class for better reusability. (for further improvement
tools like automapper can be added).

4. Property initializing:
```
public decimal VatRate { get; set; } = VatRates.UkVatRate;
```
If application uses only UkVatRate it might be good to move VatRate outside of product to avoid data duplication 
(and use it inside services that process product)
If other VatRates possible the question will be: Is the VatRate really has default value?
The questions are hard to answer without domain experts so I keep that part unchanged.

5. SubmitApplicationFor() return type is just int which under the hood is two separate things:
applicationIds and error codes. It might be good to separate it. But for the purpose of backward compatibility
I leave it same for now.

#Refactoring Plan
1. It is good to cover existing functionality with tests before refactoring, so it will be easier to know
whether refactoring broke something.
2. Refactor products introducing commands and command handlers to fix Top 1 concern.
Write tests prior to code where applicable(TDD).
3. Refactor mappings between data models.

#Further Improvements
1. Create project with dependencies registration.
2. Add tests that verifies that each command has related command handler.