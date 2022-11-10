$letters = 65..90 | % { [char]$_ }
$banned = @("A", "E", "R", "t", "u", "o", "p", "g")

foreach ($letter in $letters) {
        if ($banned -contains $letter) {
            # do nothing, move on
        } else {
            $word = $letter + "I" + "M" + "ID"
            write-host $word
        }
    
    # foreach ($letter2 in $letters){        

    # }
}