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


SELECT

  new Select()
    [
        FirstColumn.Top(10).As(FirstColumnAlias),
        SecondColumn.From(SomeTable).As(FirstColumnAlias),

    ]
    .From(SomeTable.As(tableAlias))


INNER JOIN

  var innerSelect = new Select(
    [
        SecondColumn.Top(1)
    ].From(SecondTable);

  new InnerJoin()
    [
        FirstTable.On(FirstColumn.IsEqualTo(SecondColumn)).AndOn(SecondColumn.IsEqualTo(FirstColumn)),
        FirstTable.On(FirstColumn.Matches(innerSelect)),
        SecondTable.On(SecondColumn.IsEqualTo(FirstColumn))
    ]


WHERE

  new Where()
     [
         FirstColumn.IsEqualTo(FirstValue).And(SecondColumn.IsEqualTo(SecondValue).Or(ThirdColumn.IsEqualTo(ThirdValue))),
         With.Or(FirstColumn.IsEqualTo(FirstValue).Or(ThirdColumn.IsEqualTo(ThirdValue)))
     ]


GROUP BY

  new GroupBy()
      [
          FirstColumn,
          SecondColumn.From(FirstTable)
      ]
