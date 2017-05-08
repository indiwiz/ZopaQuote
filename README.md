# ZopaQuote

## Prupose
To demonstrates use of the following: 
1. .Net Core
2. C# 7
3. xUnit
4. TDD
5. SOLID 

## Solution
There are two branches. 
* [Master](https://github.com/indiwizsol/ZopaQuote) branch has elaborate solution where some of the design decisions were made just to demonstrate a pattern or technology features. Instead of using different project (which would be the case if this would have been a bigger project with external dependencies), I have arranged code files with in folders to keep it simple. However, by using dependency injection I have kept things de-coupled, which means that if required the files can be saparated out in  different projects with ease. 
* [QuickSolution](https://github.com/indiwizsol/ZopaQuote/tree/QuickSolution) was initial one hour take at the requirements. 

## How to Install
> Checkout the [Release v1.0.1](https://github.com/indiwizsol/ZopaQuote/releases/tag/v1.0.1)


## Requirements

> There is a need for a rate calculation system allowing prospective borrowers to obtain a quote from our pool of lenders for 36 month loans. This system will take the form of a command-line application.

> You will be provided with a file containing a list of all the offers being made by the lenders within the system in CSV format, see the example market.csv file provided alongside this specification.

> You should strive to provide as low a rate to the borrower as is possible to ensure that Zopa's quotes are as competitive as they can be against our competitors'. You should also provide the borrower with the details of the monthly repayment amount and the total repayment amount.

> Repayment amounts should be displayed to 2 decimal places and the rate of the loan should be displayed to one decimal place.

> Borrowers should be able to request a loan of any £100 increment between £1000 and £15000 inclusive. If the market does not have sufficient offers from lenders to satisfy the loan then the system should inform the borrower that it is not possible to provide a quote at that time.

> The application should take arguments in the form

    cmd> [application] [market_file] [loan_amount]

> Example

    cmd> quote.exe market.csv 1500

>The application should produce output in the form
```
    cmd [application] [market_file] [loan_amount]
    Requested amount: £XXXX
    Rate: X.X%
    Monthly repayment: £XXXX.XX
    Total repayment: £XXXX.XX
```
>Example
```
  cmd quote.exe market.csv 1000
	Requested amount: £1000
	Rate: 7.0%
	Monthly repayment: £30.78
	Total repayment: £1108.10
 ```
 
 ## Sample MarketData.csv
```
Lender,Rate,Available
Bob,0.075,640
Jane,0.079,480
Fred,0.076,520
Mary,0.104,170
John,0.081,320
Dave,0.070,140
Angela,0.071,60
```

### Assumptions
* It is assumed that the valid csv file has first line as the header line. 
* Also it is assumed that the AvailableAmount in the CSV is literal. This assumption was made due to the numbers in the sample file were lower than the minimum amount that can be borrowed (1000).
* Any line other than the header line is assumed as data. If the line doesn't have 3 values or the second and third values are not valid numbers, the line is ignored. This will appear in the log file as a warning.


