
INSERT
    
     new Insert(SomeTable)
       [
           FirstColumn.WillBe(_fakeValue),
           SecondColumn.WillBe(SecondValue)
       ];


UPDATE

    new Update(SomeTable)
        [
            FirstColumn.WillBe(FirstValue),
            SecondColumn.WillBe(SecondValue)
        ]

DELETE
     
    var theMemberIdIsEqualToTheTestId = 
         new Where()
           [
             theIdIsEqualToAnItemInAnIdList.ToArray()
           ];

    new Delete(EmployeeLookupTable.Name)
        .Where(theMemberIdIsEqualToTheTestId)
        .ConnectTo(DbUtil.DefaultConnectionString)
        .Run();
