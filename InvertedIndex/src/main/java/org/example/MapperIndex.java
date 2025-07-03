package org.example;

import java.io.IOException;
import java.util.StringTokenizer;
import org.apache.hadoop.io.LongWritable;
import org.apache.hadoop.io.Text;
import org.apache.hadoop.mapreduce.Mapper;
import org.apache.hadoop.mapreduce.lib.input.FileSplit;

public class MapperIndex extends Mapper<LongWritable, Text, Text, Text> {
    private Text keyInfo = new Text();
    private Text valueInfo = new Text("1");
    private String currentUrl = "";

    @Override
    protected void map(LongWritable key, Text value, Context context)
            throws IOException, InterruptedException {

        String line = value.toString();

        // Extract URL if this is the first line
        if (key.get() == 0 && line.startsWith("http")) {
            currentUrl = line.trim();
            return;
        }

        // Process words if we have a URL
        if (!currentUrl.isEmpty()) {
            StringTokenizer tokenizer = new StringTokenizer(line);
            while (tokenizer.hasMoreTokens()) {
                String word = tokenizer.nextToken()
                        .toLowerCase()
                        .replaceAll("[^a-zA-Z0-9]", "");

                if (!word.isEmpty()) {
                    keyInfo.set(word + "@" + currentUrl);
                    context.write(keyInfo, valueInfo);
                }
            }
        }
    }
}