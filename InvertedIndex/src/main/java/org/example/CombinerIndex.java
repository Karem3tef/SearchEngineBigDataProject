package org.example;

import java.io.IOException;
import org.apache.hadoop.io.Text;
import org.apache.hadoop.mapreduce.Reducer;

public class CombinerIndex extends Reducer<Text, Text, Text, Text> {
    private final Text urlCountValue = new Text();

    @Override
    protected void reduce(Text key, Iterable<Text> values, Context context)
            throws IOException, InterruptedException {

        int sum = 0;
        for (Text value : values) {
            sum += Integer.parseInt(value.toString());
        }

        String[] parts = key.toString().split("@");
        String word = parts[0];
        String url = parts[1];

        urlCountValue.set(url + ":" + sum);
        context.write(new Text(word), urlCountValue);
    }
}