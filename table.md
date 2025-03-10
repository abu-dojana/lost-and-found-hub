| Test ID | Test Name                                         | Description                                          | Expected Result          | Actual Result | Status |
|---------|---------------------------------------------------|------------------------------------------------------|--------------------------|---------------|--------|
| TC-001  | ContainsSimilarKeywords_WithMatchingWords_ReturnsTrue | Tests keyword matching with overlapping words        | True                     | True          | Pass   |
| TC-002  | ContainsSimilarKeywords_WithNoMatchingWords_ReturnsFalse | Tests keyword matching with no overlapping words     | False                    | False         | Pass   |
| TC-003  | ContainsSimilarKeywords_WithEmptyStrings_ReturnsFalse   | Tests keyword matching with empty strings            | False                    | False         | Pass   |
| TC-004  | LevenshteinDistance_WithIdenticalStrings_ReturnsZero     | Tests distance calculation with identical strings    | 0                        | 0             | Pass   |
| TC-005  | LevenshteinDistance_WithDifferentStrings_ReturnsCorrectDistance | Tests distance calculation with different strings | 3                        | 3             | Pass   |
| TC-006  | LevenshteinDistance_WithEmptyStrings_ReturnsCorrectValue | Tests distance calculation with empty strings        | Length of non-empty string | Correct length | Pass   |
| TC-007  | ViewMatch_ReturnsViewWithMatchViewModel                  | Tests controller returns correct view with model     | ViewModel instance       | ViewModel instance | Pass   |
| TC-008  | ViewMatch_WithMissingFoundItem_HandlesGracefully         | Tests controller handles missing found item          | Not found result         | Exception      | Fail   |
| TC-009  | ViewMatch_WithMissingLostItem_HandlesGracefully          | Tests controller handles missing lost item           | Not found result         | Exception      | Fail   |
| TC-010  | MatchViewModel_PopulatesCorrectly                        | Tests model populates with correct data              | Data matches             | Data matches   | Pass   |
| TC-011  | MatchViewModel_SetsNullForMissingItems                   | Tests model handles missing items                    | Null property            | Null property  | Pass   |
| TC-012  | ItemName_Comparison_WorksCorrectly                       | Tests item name comparison logic                     | True for similar names   | True           | Pass   |
